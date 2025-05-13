using AquaEnergyMonitor.Models;
using AquaEnergyMonitor.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AquaEnergyMonitor.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly SessionService _sessionService;

        public AuthService(AppDbContext context, SessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public async Task<bool> Register(Usuario user)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == user.Email))
                return false;

            user.Id = Guid.NewGuid();

            // Hash da senha recomendado aqui
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.Senha != password) return false;

            _sessionService.SetUserId(user.Id);

            return true;
        }
    }
}
