using System.Collections.Generic;

namespace ProyectoSalud.API.Dtos
{
    public class DataForAuthenticationDto
    {
        public string Token { get; set; }
        public UserForDetailedDto User { get; set; }
        public IEnumerable<RolsUserForDetailedDto> Courses { get; set; }
    }
}