using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPerson(int userId);
    }
}