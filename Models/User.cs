using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoSalud.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string ValidationCode { get; set; }
        public DateTime ValidationDate { get; set; }
        public string Email { get; set; }
        public int LoginAttemptCounter { get; set; }
        public string UserState { get; set; }
        public string Nickname { get; set; }
        public DateTime LastActive { get; set; }
        public string RecoveryKey { get; set; }
        public DateTime RecoveryDate { get; set; }
        public Boolean VerifiedEmail { get; set; }
        public string VerifiedEmailKey { get; set; }
        public DateTime VerifyEmailDate { get; set; }
        public virtual Person Person { get; set; }
        public int PersonId { get; set; }
        public Speciality Speciality { get; set; }
        public int? SpecialityId { get; set; }
        public virtual ICollection<UserRol> UserRols { get; set; }
        // History params
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreationUserId { get; set; }
        public int UpdateUserId { get; set; }
    }
}