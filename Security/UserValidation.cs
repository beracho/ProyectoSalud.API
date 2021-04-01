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
    }
}