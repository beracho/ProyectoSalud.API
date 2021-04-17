using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalHistoryController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMedicalHistoryRepository _medicalHistoryRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicalHistoryController> _logger;
        public MedicalHistoryController(DataContext context, IMapper mapper, ILogger<MedicalHistoryController> logger, IMedicalHistoryRepository medicalHistoryRepo)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _medicalHistoryRepo = medicalHistoryRepo;
        }


        [HttpGet("{MedicalHistoryId}")]
        public async Task<IActionResult> GetConsultationsByMedicalHistory(int MedicalHistoryId)
        {
            try
            {
                var medicalHistoryFromRepo = await _medicalHistoryRepo.GetMedicalHistoryById(MedicalHistoryId);
                if (medicalHistoryFromRepo == null)
                {
                    return BadRequest("medical_history_not_found");
                }
                
                var consultationsPerMedicalHistory = await _medicalHistoryRepo.GetConsultationsPerMedicalHistory(MedicalHistoryId);

                return Ok(consultationsPerMedicalHistory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("error_on_execution");
            }
        }
    }
}