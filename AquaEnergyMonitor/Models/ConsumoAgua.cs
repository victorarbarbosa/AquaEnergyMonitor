﻿namespace AquaEnergyMonitor.Models
{
    public class ConsumoAgua
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public double ConsumoMetrosCubicos { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
