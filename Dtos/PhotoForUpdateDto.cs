using Microsoft.AspNetCore.Http;

namespace ProyectoSalud.API.Dtos
{
    public class PhotoForUpdateDto
    {
        public IFormFile ImageFile { get; set; }
    }
}