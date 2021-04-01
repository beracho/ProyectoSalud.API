using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserForDetailedDto> SearchUserByMail(string email);
        Task<User> UpdateUser(User user, Location location, Telephone telephone);
        Task<User> UpdateUser(User user);
    }
}