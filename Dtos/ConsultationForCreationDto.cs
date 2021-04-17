using System;

namespace ProyectoSalud.API.Dtos
{
    public class ConsultationForCreationDto
    {
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public DateTime ConsultationDate { get; set; }
        public int DoctorId { get; set; }
        public int MedicalHistoryId { get; set; }
        public int ConsultingRoomId { get; set; }
        public int CreationUserId { get; set; }
        public ConsultationForCreationDto()
        {
            ConsultationDate = DateTime.Now;
        }
    }
}