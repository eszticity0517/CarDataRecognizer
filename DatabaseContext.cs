using CarDataRecognizer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDataRecognizer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Adat> Adatok { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adat>(b =>
            {
                b.HasIndex(e => new { e.Date, e.PlateNumber }).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
