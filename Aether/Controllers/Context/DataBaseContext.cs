using System.Configuration;
using Aether.Models;
using Microsoft.EntityFrameworkCore;

namespace Aether.Controllers.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Adoption> Adoption { get; set; }
        public DbSet<AdoptionQueue> AdoptionQueue { get; set; }
        public DbSet<AdoptionStatus> AdoptionStatus { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<AnimalAddress> AnimalAddress { get; set; }
        public DbSet<AnimalColor> AnimalColor { get; set; }
        public DbSet<AnimalGender> AnimalGender { get; set; }
        public DbSet<AnimalPhoto> AnimalPhoto { get; set; }
        public DbSet<AnimalSize> AnimalSize { get; set; }
        public DbSet<AnimalTemperament> AnimalTemperament { get; set; }
        public DbSet<AnimalType> AnimalType { get; set; }
        public DbSet<AnimalVaccine> AnimalVaccine { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Vaccine> Vaccine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}