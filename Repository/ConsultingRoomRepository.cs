using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository
{
    public class ConsultingRoomRepository : IConsultingRoomRepository
    {
        private readonly DataContext _context;
        public ConsultingRoomRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ConsultingRoom>> GetConsultingRoomsList()
        {
            var consultingRoomsList = await _context.ConsultingRooms
            .Include(cr => cr.Location)
            .Include(cr => cr.Telephone)
            .ToListAsync();
            return consultingRoomsList;
        }
    }
}