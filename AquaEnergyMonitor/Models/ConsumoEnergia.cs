namespace AquaEnergyMonitor.Models
{
    public class ConsumoEnergia
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public double ConsumoKiloWatts { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
