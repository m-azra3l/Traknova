using Microsoft.EntityFrameworkCore;
using TelematicService.Application.Converters;
using TelematicService.Domain.Entities;

namespace TelematicService.Infrastructure.Contexts
{
    // Application DbContext for EF Core
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // Configure model builder to use ulid converter
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ulidConverter = new UlidToBytesConverter();

            modelBuilder.Entity<CrashDetection>(entity =>
            {
                entity.Property(e => e.Id)
                      .HasConversion(ulidConverter)
                      .HasColumnType("binary(16)")
                      .IsRequired();

                entity.Property(e => e.TelematicsId)
                      .HasConversion(ulidConverter)
                      .HasColumnType("binary(16)")
                      .IsRequired();
            });

            modelBuilder.Entity<Telematics>(entity =>
            {
                entity.Property(e => e.Id)
                      .HasConversion(ulidConverter)
                      .HasColumnType("binary(16)")
                      .IsRequired();
            });
        }

        // DbSet representing the CrashDetection table in the database
        public DbSet<CrashDetection> CrashDetections { get; set; }

        // DbSet representing the Telematics table in the database
        public DbSet<Telematics> Telematics { get; set; }
    }
}
