using System;
using System.Collections.Generic;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Ci { get; set; }
        public string ExpeditionCi { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Nickname { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
    }
}