using System.Threading.Tasks;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class TelephoneRepository : ITelephoneRepository
    {
        private readonly DataContext _context;
        public TelephoneRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Telephone> CreateTelephone(Telephone newTelephone)
        {
            await _context.AddAsync(newTelephone);
            await _context.SaveChangesAsync();

            return newTelephone;
        }
    }
}