using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProyectoSalud.API.Helpers;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Smtp;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Dtos;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ProyectoSalud.API.Data
{
    public class MainRepository : IMainRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public MainRepository(DataContext context, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _context = context;
            _mapper = mapper;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        // public async Task<Like> GetLike(int userId, int recipientId)
        // {
        //     return await _context.Likes.FirstOrDefaultAsync(u => u.LikerId == userId && u.LikeeId == recipientId);
        // }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> GetUser(string usernameOrEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(p => p.Photos).OrderByDescending(u => u.LastActive).AsQueryable();
            users = users.Where(user => user.Id != userParams.UserId);
            users = users.Where(user => user.Gender == userParams.Gender);
            // if (userParams.Likers)
            // {
            //     var userLikers = await GetUserLikes(userParams.UserId, userParams.Likers);
            //     users = users.Where(u => userLikers.Contains(u.Id));
            // }

            // if (userParams.Likees)
            // {
            //     var userLikees = await GetUserLikes(userParams.UserId, userParams.Likers);
            //     users = users.Where(u => userLikees.Contains(u.Id));
            // }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge - 1);
                users = users.Where(user => user.DateOfBirth >= minDob && user.DateOfBirth <= maxDob);
            }
            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public int GetCountries()
        {
            var counter = _context.Countries.Count();
            return counter;
        }

        public async Task<Country> GetCountry(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
            // _context.Countries.Count();
            return country;
        }
        public async Task<City> GetCity(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            // _context.Countries.Count();
            return city;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> UpdateUser(User user, Location location, Telephone telephone)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    location.Id = user.LocationId;
                    telephone.Id = user.TelephoneId;
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

        public async Task<string> ChangeUserRegister(string username)
        {
            var users = await _context.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();

            var useres = await _context.Users
            .Select(u => u.Username)
            .ToListAsync();

            int cont = 0;
            if (users != null)
            {
                var prim = _context.Users.FirstOrDefault(u => u.Username == username);
                while (prim != null)
                {
                    cont = cont + 1;
                    prim = _context.Users.FirstOrDefault(u => u.Username == username + cont);

                };
                username = username + cont;

                return username;
            }
            return username;
        }
    }
}