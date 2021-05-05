using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PersonRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Person> GetPerson(int userId)
        {
            var personFromRepo = await _context.Persons.FirstOrDefaultAsync(p => p.Id == userId);

            return personFromRepo;
        }

        public async Task<Person> FindPersonByCi(string ci)
        {
            var personFromRepo = await _context.Persons.FirstOrDefaultAsync(p => p.Ci == ci);

            return personFromRepo;
        }

        public async Task<Person> CreatePerson(Person personToCreate)
        {
            _context.Persons.Add(personToCreate);
            await _context.SaveChangesAsync();

            return personToCreate;
        }

        public async Task<Person> UpdatePerson(Person personToUpdate)
        {
            _context.Persons.Update(personToUpdate);
            await _context.SaveChangesAsync();

            return personToUpdate;
        }
    }
}