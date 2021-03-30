namespace ProyectoSalud.API.Models
{
    public class Telephone
    {
        public int Id { get; set; }
        public string Country  {get; set; }
        public int CountryCode { get; set; }
        public int NationalNumber { get; set; }
        public string Number { get; set; }
    }
}