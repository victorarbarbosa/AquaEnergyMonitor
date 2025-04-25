using AquaEnergyMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace AquaEnergyMonitor.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
