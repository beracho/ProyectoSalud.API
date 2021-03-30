// using System.Collections.Generic;
// using System.Threading.Tasks;
// using ProyectoSalud.API.Dtos;
// using ProyectoSalud.API.Models;
using System.Threading.Tasks;

namespace ProyectoSalud.API.Data
{
    public interface ISeedRepository
    {
        void SeedFakeData();
        void SeedAll();
    }
}