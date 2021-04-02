using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoSalud.API.Data;

namespace ProyectoSalud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultingRoomController : ControllerBase
    {
        private readonly IConsultingRoomRepository _repo;
        private readonly ILogger<ConsultingRoomController> _logger;
        public ConsultingRoomController(ILogger<ConsultingRoomController> logger, IConsultingRoomRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultingRooms()
        {
            try
            {
                var consultingRooms = await _repo.GetConsultingRoomsList();

                return Ok(consultingRooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("listing_failed");
            }
        }
    }
}