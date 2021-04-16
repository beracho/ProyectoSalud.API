using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;
        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<City> CreateCity(City newCity)
        {
            var cityFromRepo = await _context.Cities.FirstOrDefaultAsync(c => c.Name == newCity.Name);

            if (cityFromRepo != null)
            {
                return cityFromRepo;
            }
            await _context.AddAsync(newCity);
            await _context.SaveChangesAsync();

            return newCity;
        }
    }
}