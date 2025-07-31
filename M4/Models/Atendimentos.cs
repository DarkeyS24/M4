using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace M4.Models
{
    public partial class Atendimentos
    {
        public double? Id { get; set; }
        public double? PacienteId { get; set; }
        public double? TipoAtendimentoId { get; set; }
        public double? HospitalOrigemId { get; set; }
        public double? UnidadeDestinoId { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public DateTime? DataIncioTratamento { get; set; }
        public DateTime? DataTerminoTratamento { get; set; }
        public double? ResponsavelId { get; set; }
        public double? StatusClinicoId { get; set; }
        public double? CustoTratamento { get; set; }
        public double? CoberturasAdicionais { get; set; }
        public double? TaxasHospitalares { get; set; }
        public double? TransferenciaInterna { get; set; }
        public DateTime? DocsMedicosValidade { get; set; }
        public DateTime? PlanoSaudeValidade { get; set; }

        public virtual Usuarios Paciente { get; set; }
        public virtual Usuarios Responsavel { get; set; }
        public virtual StatusClinico StatusClinico { get; set; }
        public virtual TiposAtendimento TipoAtendimento { get; set; }
        public virtual Cidades HospitalOrigem { get; set; }
        public virtual Cidades UnidadeDestino { get; set; }
    }
}
