using AquaEnergyMonitor.Models;
using AquaEnergyMonitor.Persistence;
using static AquaEnergyMonitor.Components.Pages.Home;

namespace AquaEnergyMonitor.Services
{
    public class ConsumoService
    {
        private readonly AppDbContext _context;
        private readonly SessionService _sessionService;

        public ConsumoService(AppDbContext context, SessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public async Task<double> CalculaConsumoAguaAdequadoAsync()
        {
            var userId = _sessionService.UserId;
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id.Equals(userId));

            if (usuario == null)
                return 0;

            return usuario.QuantPessoas * 4.5;
        }

        public async Task<double> CalculaConsumoEnergiaAdequadoAsync()
        {
            var userId = _sessionService.UserId;
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id.Equals(userId));

            if (usuario == null)
                return 0;

            return usuario.QuantPessoas * 70.0; // 70 kWh por pessoa
        }

        public List<ConsumoAguaDto> GetConsumoAgua()
        {
            return _context.ConsumoAgua.Select(c => new ConsumoAguaDto { Id = c.Id, Data = c.Data, MetrosCubicos = c.ConsumoMetrosCubicos })
                .OrderByDescending(c => c.Data)
                .ToList();
        }

        public List<ConsumoEnergiaDto> GetConsumoEnergia()
        {
            return _context.ConsumoEnergia.Select(c => new ConsumoEnergiaDto { Id = c.Id, Data = c.Data, Kwh = c.ConsumoKiloWatts })
                .OrderByDescending(c => c.Data)
                .ToList();
        }

        public List<ConsumoAguaPresentation> GetConsumoAguaPresentation()
        {
            return _context.ConsumoAgua.Where(c => c.Data >= DateTime.Today.AddYears(-1)).ToList()
                .Select(c => new
                {
                    MesAno = new DateTime(c.Data.Year, c.Data.Month, 1),
                    c.ConsumoMetrosCubicos
                })
                .GroupBy(x => x.MesAno)
                .Select(g => new ConsumoAguaPresentation
                {
                    Mes = g.Key,
                    ConsumoTotal = g.Sum(c => c.ConsumoMetrosCubicos)
                })
                .OrderBy(x => x.Mes)
                .ToList();
        }

        public async Task<bool> CadastraConsumoAgua(ConsumoAguaDto cadastroAgua)
        {
            var userId = _sessionService.UserId;

            var user = _context.Usuarios.FirstOrDefault(u => u.Id.Equals(userId));

            if (user == null) return false;

            var consumoAgua = new ConsumoAgua
            {
                Id = Guid.NewGuid(),
                Data = cadastroAgua.Data,
                ConsumoMetrosCubicos = cadastroAgua.MetrosCubicos,
                Usuario = user
            };

            _context.ConsumoAgua.Add(consumoAgua);
            _context.SaveChanges();

            return true;
        }

        public List<ConsumoEnergiaPresentation> GetConsumoEnergiaPresentation()
        {
            return _context.ConsumoEnergia.Where(c => c.Data >= DateTime.Today.AddYears(-1)).ToList()
                .Select(c => new
                {
                    MesAno = new DateTime(c.Data.Year, c.Data.Month, 1),
                    c.ConsumoKiloWatts
                })
                .GroupBy(x => x.MesAno)
                .Select(g => new ConsumoEnergiaPresentation
                {
                    Mes = g.Key,
                    ConsumoTotal = g.Sum(c => c.ConsumoKiloWatts)
                })
                .OrderBy(x => x.Mes)
                .ToList();
        }

        public async Task<bool> CadastraConsumoEnergia(ConsumoEnergiaDto cadastroEnergia)
        {
            var userId = _sessionService.UserId;

            var user = _context.Usuarios.FirstOrDefault(u => u.Id.Equals(userId));

            if (user == null) return false;

            var consumoEnergia = new ConsumoEnergia
            {
                Id = Guid.NewGuid(),
                Data = cadastroEnergia.Data,
                ConsumoKiloWatts = cadastroEnergia.Kwh,
                Usuario = user
            };

            _context.ConsumoEnergia.Add(consumoEnergia);
            _context.SaveChanges();

            return true;
        }

        public void ExcluiConsumoAgua(Guid consumoId)
        {
            var consumo = _context.ConsumoAgua.FirstOrDefault(c => c.Id.Equals(consumoId));
            _context.ConsumoAgua.Remove(consumo);
            _context.SaveChanges();
        }

        public void ExcluiConsumoEnergia(Guid consumoId)
        {
            var consumo = _context.ConsumoEnergia.FirstOrDefault(c => c.Id.Equals(consumoId));
            _context.ConsumoEnergia.Remove(consumo);
            _context.SaveChanges();
        }

        public void AtualizaConsumoAgua(ConsumoAguaDto dto)
        {
            var entity = _context.ConsumoAgua.FirstOrDefault(x => x.Id == dto.Id);
            if (entity != null)
            {
                entity.Data = dto.Data;
                entity.ConsumoMetrosCubicos = dto.MetrosCubicos;
                _context.SaveChanges();
            }
        }

        public void AtualizaConsumoEnergia(ConsumoEnergiaDto dto)
        {
            var entity = _context.ConsumoEnergia.FirstOrDefault(x => x.Id == dto.Id);
            if (entity != null)
            {
                entity.Data = dto.Data;
                entity.ConsumoKiloWatts = dto.Kwh;
                _context.SaveChanges();
            }
        }

    }

    public class ConsumoAguaPresentation
    {
        public Guid Id { get; set; }
        public DateTime Mes { get; set; }
        public double ConsumoTotal { get; set; }
        public string MesFormatado
        {
            get
            {
                return Mes.ToString("MM/yyyy");
            }
        }
    }

    public class ConsumoEnergiaPresentation
    {
        public Guid Id { get; set; }
        public DateTime Mes { get; set; }
        public double ConsumoTotal { get; set; }
        public string MesFormatado
        {
            get
            {
                return Mes.ToString("MM/yyyy");
            }
        }
    }
}
