using System;

namespace ProyectoSalud.API.Models
{
    public class ConsultingRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Telephone Telephone { get; set; }
        public int TelephoneId { get; set; }
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual User CreationUser { get; set; }
        public int CreationUserId { get; set; }
        public virtual User UpdateUser { get; set; }
        public int UpdateUserId { get; set; }
    }
}