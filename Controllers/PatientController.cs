using System;
using System.Collections.Generic;
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
    public class PatientController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IInsureRepository _insureRepo;
        private readonly IPersonRepository _personRepo;
        private readonly ICityRepository _cityRepo;
        private readonly ITelephoneRepository _telephoneRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientController> _logger;
        public PatientController(DataContext context, IMapper mapper, ILogger<PatientController> logger, IInsureRepository insureRepo, IPersonRepository personRepo, ICityRepository cityRepo, ITelephoneRepository telephoneRepo)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _insureRepo = insureRepo;
            _personRepo = personRepo;
            _cityRepo = cityRepo;
            _telephoneRepo = telephoneRepo;
        }

        [Authorize(Roles = "admin, doctor")]
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            try
            {
                var patients = await _insureRepo.GetPatients();
                var patientList = _mapper.Map<List<PatientToListDto>>(patients);
                return Ok(patientList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("error_on_execution");
            }
        }

        [Authorize(Roles = "admin, nurse")]
        [HttpPost]
        public async Task<IActionResult> RegisterPatient(PatientRegistrationDto PatientRegistration)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    PatientRegistration.CreationUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    PatientRegistration.UpdateUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    // Check CI exists
                    var ciExists = await _personRepo.FindPersonByCi(PatientRegistration.Ci);
                    if (ciExists != null)
                    {
                        return BadRequest("ci_already_exists");
                    }

                    // Create insure
                    var insureToCreate = _mapper.Map<Insure>(PatientRegistration);
                    insureToCreate.RegistrationNumber = _insureRepo.GenerateRegistrationNumber(PatientRegistration.Name, PatientRegistration.LastName, PatientRegistration.BirthDate);
                    insureToCreate.Type = "patient";
                    var newInsure = await _insureRepo.CreateInsure(insureToCreate);

                    // Create telephone
                    var telephoneToCreate = new Telephone()
                    {
                        Number = PatientRegistration.TelephoneString
                    };
                    var newTelephone = await _telephoneRepo.CreateTelephone(telephoneToCreate);

                    // Create cellphone
                    var cellphoneToCreate = new Telephone()
                    {
                        Number = PatientRegistration.CellPhoneString
                    };
                    var newCellphone = await _telephoneRepo.CreateTelephone(cellphoneToCreate);

                    // Create person
                    var personToCreate = _mapper.Map<Person>(PatientRegistration);
                    personToCreate.InsureId = newInsure.Id;
                    personToCreate.TelephoneId = newTelephone.Id;
                    personToCreate.CellPhoneId = newCellphone.Id;
                    var newPerson = await _personRepo.CreatePerson(personToCreate);

                    var patientCreated = _mapper.Map<PatientToReturnDto>(newPerson);
                    transaction.Commit();

                    return Ok(patientCreated);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError(ex.Message);
                    return BadRequest("update_failed");
                }
            }
        }
    }
}