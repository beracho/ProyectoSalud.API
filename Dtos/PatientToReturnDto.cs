using System;

namespace ProyectoSalud.API.Dtos
{
    public class PatientToReturnDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ci { get; set; }
        public string ExpeditionCi { get; set; }
        public string Gender { get; set; }
        public string Ocupation { get; set; }
        public string PhotoUrl { get; set; }
        public string CivilStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Regional { get; set; }
        public string BirthState { get; set; }
        public string BirthCity { get; set; } //
        public string BloodType { get; set; }
        public string Type { get; set; } //
        public string RegistrationNumber { get; set; } //
        public string Kinship { get; set; } //
        public string Observations { get; set; } //
        public string PathologicalBackground { get; set; } //
        public int? LocationId { get; set; }
        public string Telephone { get; set; } //
        public string CellPhone { get; set; } //
        public int InsureId { get; set; }
        public int MedicalHistoryId { get; set; }
    }
}