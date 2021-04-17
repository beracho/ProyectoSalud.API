using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository.Interfaces
{
    public interface IConsultationRepository
    {
        Task<Consultation> CreateConsultation(Consultation consultationForCreation);
    }
}