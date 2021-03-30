using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSalud.API.Security
{
    public class UserValidation : IUserValidation
    {
        private readonly DataContext _context;

        public UserValidation(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateUserRol(int userId, string rolName, int courseId)
        {
            var userRols = await _context.UserRols.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.Rol.Name == rolName);

            if (userRols != null)
            {
                return false;
            }
            return true;
        }

        public bool isStudent(IEnumerable<Claim> tokenRols)
        {
            var rolStudentExists = false;

            var rols = new List<string>();
            foreach (var rol in tokenRols)
            {
                rols.Add(rol.Value);
                if (rol.Value == "student")
                {
                    rolStudentExists = true;
                }
            }

            return rolStudentExists;
        }
    }
}