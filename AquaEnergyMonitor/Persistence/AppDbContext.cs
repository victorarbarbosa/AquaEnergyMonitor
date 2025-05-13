using AquaEnergyMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace AquaEnergyMonitor.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<ConsumoAgua> ConsumoAgua => Set<ConsumoAgua>();
        public DbSet<ConsumoEnergia> ConsumoEnergia => Set<ConsumoEnergia>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ConsumoAgua)
                .WithOne(ca => ca.Usuario)
                .HasForeignKey(ca => ca.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.consumoEnergia)
                .WithOne(ce => ce.Usuario)
                .HasForeignKey(ce => ce.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
