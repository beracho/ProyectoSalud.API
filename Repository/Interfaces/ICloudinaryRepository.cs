using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface ICloudinaryRepository
    {
        Task<Photo> UploadImage(IFormFile imageFile, string route, int personId);
    }
}