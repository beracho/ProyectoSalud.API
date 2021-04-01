using System;
using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; }
        public int MedicalHistoryId { get; set; }
        public virtual ConsultingRoom ConsultingRoom { get; set; }
        public int ConsultingRoomId { get; set; }
        public virtual User Doctor { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<File> Files { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreationUser { get; set; }
        public int CreationUserId { get; set; }
        public virtual User UpdateUser { get; set; }
        public int UpdateUserId { get; set; }
    }
}