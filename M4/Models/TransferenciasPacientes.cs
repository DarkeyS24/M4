using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace M4.Models
{
    public partial class TransferenciasPacientes
    {
        public double? Id { get; set; }
        public double? PacienteId { get; set; }
        public double? TransferenciaVinculada { get; set; }
        public string CidadeOrigem { get; set; }
        public string CidadeDestino { get; set; }
        public DateTime? DataTransferencia { get; set; }
        public string TipoTransporte { get; set; }
        public DateTime? HoraSaida { get; set; }
        public DateTime? HoraChegada { get; set; }
        public string NivelTransporte { get; set; }
        public double? CoberturaSeguroSaude { get; set; }
        public string ServicosAdicionais { get; set; }
        public double? ValorTotalPago { get; set; }

        public virtual Usuarios Paciente { get; set; }
        public virtual TransferenciasPacientes Transferencia { get; set; }
    }
}
