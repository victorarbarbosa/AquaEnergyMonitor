using AquaEnergyMonitor.Models;
using AquaEnergyMonitor.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AquaEnergyMonitor.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;
        private readonly SessionService _sessionService;

        public UsuarioService(AppDbContext context, SessionService session)
        {
            _context = context;
            _sessionService = session;
        }

        public async Task<UsuarioDto?> GetUsuarioAsync()
        {
            var userId = _sessionService.UserId;

            if (userId == null)
                return null;

            var user = await _context.Usuarios
                .Where(u => u.Id == userId.Value)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            return new UsuarioDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Sobrenome = user.Sobrenome,
                Email = user.Email,
                Endereco = user.Endereco,
                Cep = user.Cep,
                Cidade = user.Cidade,
                Estado = user.Estado,
                Perfil = user.Perfil,
                QuantPessoas = user.QuantPessoas
            };
        }


        public UsuarioDto EditaUsuario(UsuarioDto usuario)
        {
            var usuarioOriginal = _context.Usuarios.FirstOrDefault(u => u.Id.Equals(usuario.Id));
            usuarioOriginal.Nome = usuario.Nome;
            usuarioOriginal.Sobrenome = usuario.Sobrenome;
            usuarioOriginal.Email = usuario.Email;
            usuarioOriginal.Endereco = usuario.Endereco;
            usuarioOriginal.Estado = usuario.Estado;
            usuarioOriginal.Cep = usuario.Cep;
            usuarioOriginal.Cidade = usuario.Cidade;
            usuarioOriginal.Perfil = usuario.Perfil;
            usuarioOriginal.QuantPessoas = usuario.QuantPessoas;

            _context.SaveChanges();

            return usuario;
        }
    }
}
