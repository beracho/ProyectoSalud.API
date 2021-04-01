using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using AutoMapper;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ProyectoSalud.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMainRepository _repo;
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthRepository(DataContext context, IConfiguration config, IMainRepository repo, IMapper mapper)
        {
            _context = context;
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }
        public async Task<User> Login(string usernameOrEmail, string password)
        {
            usernameOrEmail = usernameOrEmail.Trim();
            password = password.Trim();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);

            if (user == null)
                return null;

            if (!verifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }

                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            user.Username = user.Username.Trim();
            password = password.Trim();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var createdLocation = await CreateLocation();
                    user.LocationId = createdLocation.Id;
                    var createdTelephone = await CreateTelephone();
                    user.TelephoneId = createdTelephone.Id;

                    user.IsExternal = false;


                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    // TODO: Handle failure
                    // log ex.Message.lo
                    throw new Exception("registration_failed");
                }
                return user;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> UserExists(UserForRegisterDto userForRegisterDto)
        {
            var userFound = await _context.Users.FirstOrDefaultAsync(x => x.Username == userForRegisterDto.Username);
            if (userFound != null)
            {
                return userFound;
            }
            userFound = await _context.Users.FirstOrDefaultAsync(x => x.Email == userForRegisterDto.Email);
            if (userFound != null)
            {
                return userFound;
            }

            return null;
        }

        public async Task<Location> CreateLocation()
        {
            var location = new Location();
            location.CountryAddressId = 1;
            location.CityAddressId = 1;
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return location;
        }

        public async Task<Telephone> CreateTelephone()
        {
            var Telephone = new Telephone();
            await _context.Telephones.AddAsync(Telephone);
            await _context.SaveChangesAsync();

            return Telephone;
        }

        public async Task<List<Rol>> GetRolsPerUser(int userId)
        {
            var rols = await _context.UserRols
                .Where(r => r.UserId == userId)
                .Select(r => r.Rol)
                .ToListAsync();

            return rols;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }

        public string GenerateVerificationKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[16];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            string key = new String(stringChars);

            return key;
        }

        public User CompleteInfoToConfirmVerify(UserForRecoveryVerifyDto userForRecoveryVerifyDto, User user)
        {
            user.RecoveryKey = "";
            user.RecoveryDate = new DateTime();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRecoveryVerifyDto.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return user;
        }

        public async Task<string> ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var user = await _repo.GetUser(userForChangePasswordDto.UsernameOrEmail);

                    if (!verifyPasswordHash(userForChangePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
                    {
                        return "invalid_current_password";
                    }

                    byte[] NewPasswordHash, NewPasswordSalt;
                    CreatePasswordHash(userForChangePasswordDto.NewPassword, out NewPasswordHash, out NewPasswordSalt);

                    user.PasswordHash = NewPasswordHash;
                    user.PasswordSalt = NewPasswordSalt;

                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    // TODO: Handle failure
                    return "change_password_failed";
                }
                return "";
            }
        }

        public SecurityTokenDescriptor CreateToken(User userToReturn, List<Rol> rols)
        {
            try
            {
                var claims = new[] {
                    new Claim (ClaimTypes.NameIdentifier, userToReturn.Id.ToString()),
                    new Claim (ClaimTypes.Name, userToReturn.Username)
                };

                foreach (var rol in rols)
                {
                    claims = claims.Concat(
                        new[] {
                            new Claim(ClaimTypes.Role, rol.Name)
                        }).ToArray();
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenDescriptor;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<List<Rol>> AssignRol(int userId, string rolName)
        {
            var rol = await _context.Rols.FirstOrDefaultAsync(r => r.Name == rolName);
            if (rol != null)
            {
                var userRol = new UserRol()
                {
                    RolId = rol.Id,
                    UserId = userId
                };

                await _context.UserRols.AddAsync(userRol);
                await _context.SaveChangesAsync();
            }

            var rols = await GetRolsPerUser(userId);

            return rols;
        }

        public async Task<DataForAuthenticationDto> AuthenticationData(int userId)
        {
            var userFromRepo = await GetUser(userId);

            var rols = await GetRolsPerUser(userId);
            var rolsAssignedToList = _mapper.Map<List<RolsToListDto>>(rols);

            var tokenDescriptor = CreateToken(userFromRepo, rols);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var detailedUser = _mapper.Map<UserForDetailedDto>(userFromRepo);

            var dataForAuthenticationDto = new DataForAuthenticationDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = detailedUser,
                Rols = rolsAssignedToList
            };

            return dataForAuthenticationDto;
        }
    }
}