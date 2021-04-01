using System;
using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Ci { get; set; }
        public string ExpeditionCi { get; set; }
        public string Gender { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual Location Location { get; set; }
        public int? LocationId { get; set; }
        public virtual Telephone Telephone { get; set; }
        public int? TelephoneId { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual User User { get; set; }
        public virtual Insure Insure { get; set; }
        public int? InsureId { get; set; }
        public virtual MedicalHistory MedicalHistory { get; set; }
        public int? MedicalHistoryId { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreationUserId { get; set; }
        public int UpdateUserId { get; set; }
    }
}