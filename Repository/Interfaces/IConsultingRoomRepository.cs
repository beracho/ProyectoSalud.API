using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Data
{
    public interface IConsultingRoomRepository
    {
        Task<List<ConsultingRoom>> GetConsultingRoomsList();
    }
}