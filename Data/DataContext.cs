using ProyectoSalud.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSalud.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<ConsultingRoom> ConsultingRooms { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Insure> Insures { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Speciality> Specialitiess { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
        public DbSet<UserRol> UserRols { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Location>()
                .HasOne(l => l.CountryAddress)
                .WithMany(c => c.LocationAddress)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<User>(x => x.PersonId);

            builder.Entity<UserRol>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRols)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne(u => u.Speciality)
                .WithMany(u => u.Users)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Insure>()
                .HasIndex(i => i.RegistrationNumber)
                .IsUnique();

            builder.Entity<Insure>()
                .HasOne(i => i.Insurer)
                .WithMany(i => i.Insurees)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Person>()
                .HasOne(p => p.MedicalHistory)
                .WithOne(mh => mh.Patient);

            builder.Entity<MedicalHistory>()
                .HasOne(mh => mh.CreationUser);
            builder.Entity<MedicalHistory>()
                .HasOne(mh => mh.UpdateUser);
        }
    }
}