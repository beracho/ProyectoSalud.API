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
            if (_repo.GetCountries() == 0)
            {
                SeedCountries();
                SeedCities();
                SeedRols();
                if (_config.GetSection("DatabaseParams:SeedFakeData").Value == "true")
                {
                    SeedFakeData();
                }
            }
        }

        public void SeedCountries()
        {
            var countryData = System.IO.File.ReadAllText("Data/SeedData/CountrySeedData.json");
            var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
            foreach (var country in countries)
            {
                _context.Countries.Add(country);
            }
            _context.SaveChanges();
        }

        public void SeedCities()
        {
            var cityData = System.IO.File.ReadAllText("Data/SeedData/CitySeedData.json");
            var cities = JsonConvert.DeserializeObject<List<City>>(cityData);
            foreach (var city in cities)
            {
                _context.Cities.Add(city);
            }
            _context.SaveChanges();
        }

        public void SeedRols()
        {
            var rolData = System.IO.File.ReadAllText("Data/SeedData/RolSeedData.json");
            var rols = JsonConvert.DeserializeObject<List<Rol>>(rolData);
            foreach (var rol in rols)
            {
                _context.Rols.Add(rol);
            }
            _context.SaveChanges();
        }

        public void SeedFakeData()
        {
            // Add fake users and its dependencies
            SeedFakeUsers();
            SeedFakePatients();
            SeedFakeConsultingRoom();
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

                var speciality = user.Speciality;
                if (speciality != null)
                {
                    _context.Specialitiess.Add(speciality);
                }

                var userRols = user.UserRols;
                if (userRols != null)
                {
                    foreach (var userRol in userRols)
                    {
                        _context.UserRols.Add(userRol);
                    }
                }

                var person = user.Person;
                if (person != null)
                {
                    _context.Persons.Add(person);
                }
            }
            _context.SaveChanges();
        }

        public void SeedFakePatients()
        {
            var patientData = System.IO.File.ReadAllText("Data/SeedFakeData/PatientSeedData.json");
            var patients = JsonConvert.DeserializeObject<List<Person>>(patientData);

            foreach (var patient in patients)
            {
                _context.Persons.Add(patient);

                var medicalHistory = patient.MedicalHistory;
                if (medicalHistory != null)
                {
                    _context.MedicalHistories.Add(medicalHistory);
                }

                var insure = patient.Insure;
                if (insure != null)
                {
                    _context.Insures.Add(insure);
                }
            }
            _context.SaveChanges();
        }

        public void SeedFakeConsultingRoom()
        {
            var consultingRoomsData = System.IO.File.ReadAllText("Data/SeedFakeData/ConsultingRoomSeedData.json");
            var consultingRooms = JsonConvert.DeserializeObject<List<ConsultingRoom>>(consultingRoomsData);

            foreach (var consultingRoom in consultingRooms)
            {
                _context.ConsultingRooms.Add(consultingRoom);

                var telephone = consultingRoom.Telephone;
                if (telephone != null)
                {
                    _context.Telephones.Add(telephone);
                }

                var location = consultingRoom.Location;
                if (location != null)
                {
                    _context.Locations.Add(location);
                }
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