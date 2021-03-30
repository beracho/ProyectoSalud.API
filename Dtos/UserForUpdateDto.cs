using System;
using System.Collections.Generic;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Dtos
{
    public class UserForUpdateDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nickname { get; set; }
        public int CountryAddressId { get; set; }
        public int CityAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int CountryCode { get; set; }
        public int NationalNumber { get; set; }
        public string Number { get; set; }
    }
}