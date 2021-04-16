using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface ICityRepository
    {
        Task<City> CreateCity(City newCity);
    }
}