using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPerson(int userId);
        Task<Person> FindPersonByCi(string ci);
        Task<Person> CreatePerson(Person personToCreate);
        Task<Person> UpdatePerson(Person personToUpdate);
    }
}