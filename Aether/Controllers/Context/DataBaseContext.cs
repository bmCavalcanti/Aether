using System.Configuration;
using Aether.Models;
using Microsoft.EntityFrameworkCore;

namespace Aether.Controllers.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> User { get; set; }

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