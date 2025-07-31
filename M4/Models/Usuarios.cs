using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace M4.Models
{
    public partial class Usuarios
    {
        public double Id { get; set; }
        public string Email { get; set; }
        public double? Senha { get; set; }
        public double? Telefone { get; set; }
        public string Nome { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public double? Cpf { get; set; }
        public string Sexo { get; set; }
        public string TipoUsuario { get; set; }
        public double? TamanhoFamilia { get; set; }
        public double? EstadoCivil { get; set; }
    }
}
