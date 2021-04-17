using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Repository
{
    public class MedicalHistoryRepository : IMedicalHistoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MedicalHistoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MedicalHistory> GetMedicalHistoryById(int MedicalHistoryId)
        {
            var medicalHistoryFromRepo = await _context.MedicalHistories.FirstOrDefaultAsync(mh => mh.Id == MedicalHistoryId);

            return medicalHistoryFromRepo;
        }

        public async Task<List<Consultation>> GetConsultationsPerMedicalHistory(int MedicalHistoryId)
        {
            var consultationList = await _context.Consultations
            .Where(c => c.MedicalHistoryId == MedicalHistoryId)
            .Include(c => c.ConsultingRoom)
            .Include(c => c.Doctor)
            .ToListAsync();

            return consultationList;
        }
    }
}