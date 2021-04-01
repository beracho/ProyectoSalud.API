using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _authRepo;
        private readonly IMainRepository _mainRepo;
        public PersonRepository(DataContext context, IAuthRepository authRepo, IMainRepository mainRepo)
        {
            _context = context;
            _authRepo = authRepo;
            _mainRepo = mainRepo;
        }

        public async Task<Person> GetPerson(int userId)
        {
            var person = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.Person)
            .FirstOrDefaultAsync();

            return person;
        }

    }
}