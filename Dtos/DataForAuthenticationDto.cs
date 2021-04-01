using System.Collections.Generic;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Dtos
{
    public class DataForAuthenticationDto
    {
        public string Token { get; set; }
        public UserForDetailedDto User { get; set; }
        public List<RolsToListDto> Rols { get; set; }
    }
}