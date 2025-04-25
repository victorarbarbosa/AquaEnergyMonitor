using AquaEnergyMonitor.Models;
using AquaEnergyMonitor.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AquaEnergyMonitor.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Register(Usuario user)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == user.Email))
                return false;

            // Hash da senha recomendado aqui
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.Senha != password) return false;

            return true;
        }
    }
}
