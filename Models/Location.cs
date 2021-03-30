namespace ProyectoSalud.API.Models
{
    public class Location
    {
        public int Id { get; set; }
        public virtual Country CountryAddress { get; set; }
        public int CountryAddressId { get; set; }
        public virtual City CityAddress { get; set; }
        public int CityAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
    }
}