using System;
using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public DateTime ConsultationDate { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; }
        public int MedicalHistoryId { get; set; }
        public virtual ConsultingRoom ConsultingRoom { get; set; }
        public int ConsultingRoomId { get; set; }
        public virtual User Doctor { get; set; }
        public int DoctorId { get; set; }
        public virtual ICollection<File> Files { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreationUserId { get; set; }
        public int UpdateUserId { get; set; }
    }
}