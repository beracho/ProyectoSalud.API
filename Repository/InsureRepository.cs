using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class InsureRepository : IInsureRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public InsureRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Person>> GetPatients()
        {
            var insures = await _context.Persons
            .Where(i => i.Insure.Type != null)
            .Include(p => p.Insure)
            .ToListAsync();

            return insures;
        }

        public async Task<Insure> CreateInsure(Insure insureToCreate)
        {
            _context.Insures.Add(insureToCreate);
            await _context.SaveChangesAsync();

            return insureToCreate;
        }

        public string GenerateRegistrationNumber(string Name, string LastName, DateTime BirthDate)
        {
            var registrationNumber = "";

            var lastNames = LastName.Split(" ");
            foreach (var item in lastNames)
            {
                registrationNumber = registrationNumber + item.Substring(0, 1);
            }

            var names = Name.Split(" ");
            foreach (var item in names)
            {
                registrationNumber = registrationNumber + item.Substring(0, 1);
            }

            registrationNumber = registrationNumber + BirthDate.Day + BirthDate.Month + BirthDate.Year;

            return registrationNumber;
        }
    }
}