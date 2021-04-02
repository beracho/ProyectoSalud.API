using System;
using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class Insure
    {
        public int Id { get; set; }
        public string Type { get; set; } //insured - beneficiary
        public DateTime InsuranceDate { get; set; }
        public virtual Insure Insurer { get; set; }
        public int? InsurerId { get; set; }
        public virtual ICollection<Insure> Insurees { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreationUserId { get; set; }
        public int UpdateUserId { get; set; }
    }
}