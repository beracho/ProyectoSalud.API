using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IMedicalHistoryRepository
    {
        Task<MedicalHistory> GetMedicalHistoryById(int MedicalHistoryId);
        Task<List<Consultation>> GetConsultationsPerMedicalHistory(int MedicalHistoryId);
    }
}