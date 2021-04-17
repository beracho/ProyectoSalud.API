using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;
using ProyectoSalud.API.Repository.Interfaces;

namespace ProyectoSalud.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConsultationRepository _consultationRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<ConsultationController> _logger;
        public ConsultationController(DataContext context, IMapper mapper, ILogger<ConsultationController> logger, IConsultationRepository consultationRepo)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _consultationRepo = consultationRepo;
        }

        [Authorize(Roles = "doctor")]
        [HttpPost]
        public async Task<IActionResult> CreateConsultation(ConsultationForCreationDto consultationForCreation)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    consultationForCreation.CreationUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    consultationForCreation.DoctorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var newConsultation = _mapper.Map<Consultation>(consultationForCreation);
                    newConsultation = await _consultationRepo.CreateConsultation(newConsultation);

                    transaction.Commit();
                    return CreatedAtRoute("", newConsultation);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.Message);
                    return BadRequest("error_on_execution");
                }
            }
        }
    }
}