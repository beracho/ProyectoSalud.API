using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Dtos;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserForDetailedDto> SearchUserByMail(string email);
    }
}