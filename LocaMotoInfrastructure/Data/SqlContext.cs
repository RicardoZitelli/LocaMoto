using LocaMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace LocaMoto.Infrastructure.Data
{
    public sealed class SqlContext(DbContextOptions<SqlContext> options) : DbContext(options)
    {
        public DbSet<Motorcycle>? Motorcycle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define índice único para a coluna LicensePlate
            modelBuilder.Entity<Motorcycle>()
                .HasIndex(m => m.LicensePlate)
                .IsUnique();
        }
    }
}
