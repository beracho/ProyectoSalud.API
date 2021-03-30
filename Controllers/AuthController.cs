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
        private readonly IConfiguration _config;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository repo, IMainRepository mainRepo, IUserRepository userRepo, IConfiguration config, IMapper mapper, IMailService mailService, ILogger<AuthController> logger)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
            _userRepo = userRepo;
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

                string alreadyExists = await _repo.UserExists(userForRegisterDto);
                if (alreadyExists != "")
                {
                    return BadRequest(alreadyExists);
                }

                var userToCreate = _mapper.Map<User>(userForRegisterDto);

                var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
                var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser);

                var rolsAssigned = await _repo.GetRolsPerUser(createdUser.Id);
                IEnumerable<RolsUserForDetailedDto> rolsUserToReturn = _mapper.Map<IEnumerable<RolsUserForDetailedDto>>(rolsAssigned);

                var tokenDescriptor = _repo.CreateToken(userToCreate);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                try
                {
                    RegisterEmail request = new RegisterEmail();
                    request.ToEmail = userToReturn.Email;
                    request.UserName = userToReturn.Username;

                    await _mailService.SendWelcomeEmailAsync(request);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return CreatedAtRoute("GetUser",
                        new { controller = "Users", id = createdUser.Id, token = tokenHandler.WriteToken(token) },
                        new { userToReturn, token = tokenHandler.WriteToken(token), rolsUserToReturn, message = "email_failed" });
                }
                return CreatedAtRoute("GetUser",
                    new { controller = "Users", id = createdUser.Id, token = tokenHandler.WriteToken(token) },
                    new { userToReturn, token = tokenHandler.WriteToken(token), rolsUserToReturn });

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
                    return Unauthorized();
                    
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
                string alreadyExists = await _repo.UserExists(userToVerifyExist);
                if (alreadyExists == "")
                {
                    return BadRequest("user_does_not_exist");
                }

                var userToUpdate = await _mainRepo.GetUser(userForRecoveryDto.UsernameOrEmail);

                userToUpdate.RecoveryKey = _repo.GenerateVerificationKey();
                userToUpdate.RecoveryDate = DateTime.Now.AddDays(1);

                var updatedUser = await _mainRepo.UpdateUser(userToUpdate);

                RecoveryKeyEmail recoveryKeyEmail = new RecoveryKeyEmail(updatedUser.Email, updatedUser.Name, updatedUser.LastName, _mailService.GetVerifyURL(updatedUser.RecoveryKey, "auth/passwordrecovery"));
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
                string alreadyExists = await _repo.UserExists(userToVerifyExist);
                if (alreadyExists == "")
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

                var updatedUser = await _mainRepo.UpdateUser(userToUpdate);

                RecoveryKeyEmail recoveryKeyEmail = new RecoveryKeyEmail(updatedUser.Email, updatedUser.Name, updatedUser.LastName, "");
                await _mailService.SendConfirmationRecoveryEmailAsync(recoveryKeyEmail);

                var tokenDescriptor = _repo.CreateToken(updatedUser);
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var user = _mapper.Map<UserForDetailedDto>(updatedUser);

                var rolsAssigned = await _repo.GetRolsPerUser(user.Id);
                IEnumerable<RolsUserForDetailedDto> rolsUserToReturn = _mapper.Map<IEnumerable<RolsUserForDetailedDto>>(rolsAssigned);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user,
                    rolsUserToReturn
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