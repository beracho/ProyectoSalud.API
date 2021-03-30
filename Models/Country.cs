using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string PhoneCode { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public virtual ICollection<Location> LocationAddress { get; set; }
    }
}