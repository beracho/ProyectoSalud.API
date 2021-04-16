using System;
using System.Security.Claims;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Dtos
{
    public class PatientRegistrationDto
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Ci { get; set; }
        public string ExpeditionCi { get; set; }
        public string CivilStatus { get; set; }
        public string Ocupation { get; set; }
        public string Regional { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthState { get; set; }
        public string BirthCity { get; set; }
        public string Address { get; set; }
        public string TelephoneString { get; set; }
        public string CellPhoneString { get; set; }
        public string BloodType { get; set; }
        public string PathologicalBackground { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreationUserId { get; set; }
        public int UpdateUserId { get; set; }
        public PatientRegistrationDto()
        {
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}