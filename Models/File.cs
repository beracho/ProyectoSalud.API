using System;
using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}