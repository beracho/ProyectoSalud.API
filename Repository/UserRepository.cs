using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;
using ProyectoSalud.API.Services;
using ProyectoSalud.API.Smtp;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSalud.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepo;
        private readonly IMainRepository _mainRepo;
        private readonly IMailService _mailService;
        public UserRepository(DataContext context, IMapper mapper, IAuthRepository authRepo, IMainRepository mainRepo, IMailService mailService)
        {
            _context = context;
            _mapper = mapper;
            _authRepo = authRepo;
            _mainRepo = mainRepo;
            _mailService = mailService;
        }

        public async Task<UserForDetailedDto> SearchUserByMail(string email)
        {
            var user = await _context.Users.Include(u => u.Telephone).FirstOrDefaultAsync(u => u.Email == email);
            var userForEnroll = _mapper.Map<UserForDetailedDto>(user);

            return userForEnroll;
        }
    }
}