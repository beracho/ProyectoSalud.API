using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoSalud.API.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ProyectoSalud.API.Data
{
    public class Seed : ISeedRepository
    {
        private readonly DataContext _context;
        private readonly IMainRepository _repo;
        private readonly IConfiguration _config;
        public Seed(IMainRepository repo, DataContext context, IConfiguration config)
        {
            _context = context;
            _repo = repo;
            _config = config;
        }

        public void SeedAll()
        {
            // if (_repo.GetCountries() == 0)
            // {
            //     // SeedCountries();
            //     if (_config.GetSection("DatabaseParams:SeedFakeData").Value == "true")
            //     {
            //         SeedFakeData();
            //     }
            // }
        }

        // public void SeedCountries()
        // {
        //     var countryData = System.IO.File.ReadAllText("Data/SeedData/CountrySeedData.json");
        //     var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
        //     foreach (var country in countries)
        //     {
        //         _context.Countries.Add(country);
        //     }
        //     _context.SaveChanges();
        // }

        public void SeedFakeData()
        {
            // Add fake users and its dependencies
            // SeedFakeUsers();
        }

        public void SeedFakeUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/SeedFakeData/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.Users.Add(user);
            }
            _context.SaveChanges();
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}