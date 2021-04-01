using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;
using ProyectoSalud.API.Services;
using ProyectoSalud.API.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProyectoSalud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMainRepository _mainRepo;
        private readonly IUserRepository _userRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IConfiguration _config;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository repo, IMainRepository mainRepo, IPersonRepository personRepo, IUserRepository userRepo, IConfiguration config, IMapper mapper, IMailService mailService, ILogger<AuthController> logger)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
            _userRepo = userRepo;
            _personRepo = personRepo;
            _mailService = mailService;
            _mainRepo = mainRepo;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

                var userFound = await _repo.UserExists(userForRegisterDto);
                if (userFound != null)
                {
                    return BadRequest("already_exists");
                }

                var createdUser = await _repo.Register(userForRegisterDto);
                var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser);

                var rolsAssigned = await _repo.AssignRol(createdUser.Id, userForRegisterDto.Rol);
                var rolsAssignedToList = _mapper.Map<List<RolsToListDto>>(rolsAssigned);

                var tokenDescriptor = _repo.CreateToken(createdUser, rolsAssigned);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                try
                {
                    RegisterEmail request = new RegisterEmail();
                    request.ToEmail = userToReturn.Email;
                    request.UserName = userToReturn.Username;
                    request.FirstName = userToReturn.FirstName;

                    await _mailService.SendWelcomeEmailAsync(request);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return CreatedAtRoute("GetUser",
                        new { controller = "Users", id = createdUser.Id, token = tokenHandler.WriteToken(token) },
                        new { userToReturn, token = tokenHandler.WriteToken(token), rolsAssignedToList, message = "email_failed" });
                }
                return CreatedAtRoute("GetUser",
                    new { controller = "Users", id = createdUser.Id, token = tokenHandler.WriteToken(token) },
                    new { userToReturn, token = tokenHandler.WriteToken(token), rolsAssignedToList });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest("registration_failed");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                var userFromRepo = await _repo.Login(userForLoginDto.UsernameOrEmail.ToLower(), userForLoginDto.Password);

                if (userFromRepo == null)
                    return BadRequest("wrong_parameters");

                var authenticationData = await _repo.AuthenticationData(userFromRepo.Id);

                return Ok(authenticationData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("login_failed");
            }
        }

        [HttpPost("recoveryAccount")]
        public async Task<IActionResult> RecoveryAccount(UserForRecoveryDto userForRecoveryDto)
        {
            try
            {
                var userToVerifyExist = _mapper.Map<UserForRegisterDto>(userForRecoveryDto);
                var user = _mapper.Map<User>(userToVerifyExist);
                var userFound = await _repo.UserExists(userToVerifyExist);
                if (userFound == null)
                {
                    return BadRequest("user_does_not_exist");
                }

                var userToUpdate = await _mainRepo.GetUser(userForRecoveryDto.UsernameOrEmail);

                userToUpdate.RecoveryKey = _repo.GenerateVerificationKey();
                userToUpdate.RecoveryDate = DateTime.Now.AddDays(1);

                var updatedUser = await _userRepo.UpdateUser(userToUpdate);
                var person = await _personRepo.GetPerson(updatedUser.Id);

                RecoveryKeyEmail recoveryKeyEmail = new RecoveryKeyEmail(updatedUser.Email, person.Name, person.LastName, _mailService.GetVerifyURL(updatedUser.RecoveryKey, "auth/passwordrecovery"));
                await _mailService.SendRecoveryKeyEmailAsync(recoveryKeyEmail);

                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("recovery_account_failed");
            }
        }

        [HttpPost("verifyRecovery")]
        public async Task<IActionResult> VerifyRecovery(UserForRecoveryVerifyDto userForRecoveryVerifyDto)
        {
            try
            {
                var userToVerifyExist = _mapper.Map<UserForRegisterDto>(userForRecoveryVerifyDto);
                var userFound = await _repo.UserExists(userToVerifyExist);
                if (userFound == null)
                {
                    return BadRequest("user_does_not_exist");
                }

                var userToVerifyKey = await _mainRepo.GetUser(userForRecoveryVerifyDto.Email);

                if (userForRecoveryVerifyDto.VerifyKey == "")
                {

                    return BadRequest("key_does_not_valid");
                }

                if (userForRecoveryVerifyDto.VerifyKey != userToVerifyKey.RecoveryKey)
                {
                    return BadRequest("key_does_not_valid");
                }

                if (userToVerifyKey.RecoveryDate < DateTime.Now)
                {
                    return BadRequest("key_does_not_valid");
                }

                var userToUpdate = _repo.CompleteInfoToConfirmVerify(userForRecoveryVerifyDto, userToVerifyKey);

                var updatedUser = await _userRepo.UpdateUser(userToUpdate);
                var person = await _personRepo.GetPerson(updatedUser.Id);

                RecoveryKeyEmail recoveryKeyEmail = new RecoveryKeyEmail(updatedUser.Email, person.Name, person.LastName, "");
                await _mailService.SendConfirmationRecoveryEmailAsync(recoveryKeyEmail);

                var user = _mapper.Map<UserForDetailedDto>(updatedUser);

                var rolsAssigned = await _repo.GetRolsPerUser(user.Id);
                var rolsAssignedToList = _mapper.Map<List<RolsToListDto>>(rolsAssigned);

                var tokenDescriptor = _repo.CreateToken(updatedUser, rolsAssigned);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user,
                    rolsAssignedToList
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("verify_recovery_failed");
            }
        }

        //Login, mostrar roles de institución del usuario
    }
}