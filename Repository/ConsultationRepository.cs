using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly DataContext _context;
        public ConsultationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Consultation> CreateConsultation(Consultation consultationForCreation)
        {
            await _context.AddAsync(consultationForCreation);
            await _context.SaveChangesAsync();

            return consultationForCreation;
        }
    }
}