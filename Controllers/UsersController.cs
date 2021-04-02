using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Helpers;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;
using ProyectoSalud.API.Security;
using ProyectoSalud.API.Services;
using ProyectoSalud.API.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProyectoSalud.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMainRepository _repo;
        private readonly IAuthRepository _authRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly ILogger<UsersController> _logger;
        private readonly IUserValidation _userValidation;
        public UsersController(IUserRepository userRepo, IMainRepository repo, IAuthRepository authRepo, IMapper mapper, IMailService mailService, ILogger<UsersController> logger, IUserValidation userValidation)
        {
            _mapper = mapper;
            _repo = repo;
            _authRepo = authRepo;
            _logger = logger;
            _mailService = mailService;
            _userRepo = userRepo;
            _userValidation = userValidation;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            try
            {
                var CurrentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userFromRepo = await _repo.GetUser(CurrentUserId);
                userParams.UserId = CurrentUserId;
                var users = await _repo.GetUsers(userParams);
                var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
                Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("error_on_execution");
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _repo.GetUser(id);
                var userToReturn = _mapper.Map<UserForDetailedDto>(user);
                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("error_on_execution");
            }
        }

        [HttpPut("{userId}/ChangePassword")]
        public async Task<IActionResult> ChangePassword(int userId, UserForChangePasswordDto userForChangePasswordDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            try
            {
                var response = await _authRepo.ChangePassword(userForChangePasswordDto);

                if (response != "")
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("update_failed");
            }
        }
    }
}
