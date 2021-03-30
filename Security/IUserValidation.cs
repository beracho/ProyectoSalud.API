using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Security
{
    public interface IUserValidation
    {
        Task<bool> ValidateUserRol(int userId, string rolName, int courseId);
        bool isStudent(IEnumerable<Claim> tokenRols);
    }
}