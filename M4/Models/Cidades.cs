using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace M4.Models
{
    public partial class Cidades
    {
        public double? Id { get; set; }
        public string Cidade { get; set; }
        public double? EstadoId { get; set; }
        public virtual Estados Estado { get; set; }
    }
}
