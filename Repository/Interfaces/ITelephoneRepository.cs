using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface ITelephoneRepository
    {
        Task<Telephone> CreateTelephone(Telephone newTelephone);
    }
}