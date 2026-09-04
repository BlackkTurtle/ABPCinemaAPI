using System.Linq;
using System.Reflection;
using ABPCinemaAPI.DAL.Entities;
using ABPCinemaAPI.DAL.Persistence.Seeding;
using Microsoft.EntityFrameworkCore;

namespace ABPCinemaAPI.DAL.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
    {
        Database.EnsureCreated();
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void SeedData()
    {
        if (!Halls.Any())
        {
            CinemaSeeding.SeedingInit();

            Halls.AddRange(CinemaSeeding.Halls);

            SaveChanges();
        }
    }
}