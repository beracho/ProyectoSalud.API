using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IInsureRepository
    {
        Task<List<Person>> GetPatients();
        Task<Insure> CreateInsure(Insure insureToCreate);
        string GenerateRegistrationNumber(string Name, string LastName, DateTime BirthDate);
    }
}