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

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user, Location location, Telephone telephone)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    location.Id = (int)user.Person.LocationId;
                    telephone.Id = (int)user.Person.TelephoneId;
                    _context.Locations.Update(location);
                    _context.Telephones.Update(telephone);
                    _context.Users.Update(user);

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    // TODO: Handle failure
                    throw new Exception("user_update_failed");
                }
                return user;
            }
        }

        public async Task<UserForDetailedDto> SearchUserByMail(string email)
        {
            var user = await _context.Users
            .Include(u => u.Person)
            .ThenInclude(p => p.Telephone)
            .FirstOrDefaultAsync(u => u.Email == email);
            var userForEnroll = _mapper.Map<UserForDetailedDto>(user);

            return userForEnroll;
        }
    }
}