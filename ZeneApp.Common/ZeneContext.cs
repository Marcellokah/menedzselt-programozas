using Microsoft.EntityFrameworkCore;

namespace ZeneApp.Common
{
    public class ZeneContext : DbContext
    {
        public DbSet<Zene> Zenek { get; set; }

        // Konstruktor a Web projekthez (DI miatt)
        public ZeneContext(DbContextOptions<ZeneContext> options) : base(options) { }

        // Üres konstruktor a WinForms/Design-time támogatáshoz
        public ZeneContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // WinForms alapértelmezett connection string
                optionsBuilder.UseSqlite("Data Source=zeneapp.db");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Egyedi cím kényszer
            modelBuilder.Entity<Zene>()
                .HasIndex(z => z.Cim)
                .IsUnique();
        }
    }
}