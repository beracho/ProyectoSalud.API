using ProyectoSalud.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyectoSalud.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
        public DbSet<UserRol> UserRols { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Location>()
                .HasOne(l => l.CountryAddress)
                .WithMany(c => c.LocationAddress)
                .OnDelete(DeleteBehavior.Restrict);
            // builder.Entity<Price>()
            //     .HasOne(p => p.Course)
            //     .WithMany(c => c.Prices)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}