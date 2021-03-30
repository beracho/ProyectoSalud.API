using System;
using System.Collections.Generic;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Dtos
{
    public class UserForChangePasswordDto
    {
        public string UsernameOrEmail { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}