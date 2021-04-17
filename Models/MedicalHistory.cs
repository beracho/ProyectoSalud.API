using System;
using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class MedicalHistory
    {
        public int Id { get; set; }
        public virtual Person Patient { get; set; }
        public virtual ICollection<Consultation> Consultations { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreationUserId { get; set; }
        public int UpdateUserId { get; set; }
    }
}