using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Data.SqlTypes;

namespace CrudWithGeocoding.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Company 1",
                    OrganizationNumber = 1,
                    Notes = ""
                },
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Company 2",
                    OrganizationNumber = 2,
                    Notes = ""
                },
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Company 3",
                    OrganizationNumber = 3,
                    Notes = ""
                },
                new Company
                {
                    Id = Guid.NewGuid(),
                    Name = "Company 4",
                    OrganizationNumber = 4,
                    Notes = ""
                });

        }
    }
}
