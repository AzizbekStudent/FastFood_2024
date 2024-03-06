using Fast_Food.DAL.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Fast_Food.DAL.Data
{
    // Students ID: 00013836, 00014725, 00014896
    public class FastFood_DbContext : DbContext
    {
        // Constructor
        public FastFood_DbContext(DbContextOptions options) : base(options)
        {
        }

        // Models
        public DbSet<Employee> Employees { get; set; }


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(p => p.FName).HasMaxLength(200);
                entity.Property(p => p.LName).HasMaxLength(200);
                entity.Property(p => p.Job).HasMaxLength(30);
                entity.Property(p => p.Telephone).HasMaxLength(30);
            });
        }
    }
}
