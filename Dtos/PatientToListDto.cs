namespace ProyectoSalud.API.Dtos
{
    public class PatientToListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ci { get; set; }
        public string ExpeditionCi { get; set; }
        public string Type { get; set; }
        public string RegistrationNumber { get; set; }
        public string PhotoUrl { get; set; }
    }
}