using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using M4.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace M4.Context
{
    public partial class Sessao4 : DbContext
    {
        public Sessao4()
        {
        }

        public Sessao4(DbContextOptions<Sessao4> options)
            : base(options)
        {
        }

        private static Sessao4 instance;

        public static Sessao4 GetInstance()
        {
            if (instance == null)
            {
                instance = new Sessao4();
            }
            return instance;
        }

        public virtual DbSet<Atendimentos> Atendimentos { get; set; }
        public virtual DbSet<Cidades> Cidades { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCivil { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<StatusClinico> StatusClinico { get; set; }
        public virtual DbSet<TiposAtendimento> TiposAtendimento { get; set; }
        public virtual DbSet<TransferenciasPacientes> TransferenciasPacientes { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Sessao4;User Id=sa;Password=sql;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Atendimentos>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.DataIncioTratamento).HasColumnType("datetime");

                entity.Property(e => e.DataSolicitacao).HasColumnType("datetime");

                entity.Property(e => e.DataTerminoTratamento).HasColumnType("datetime");

                entity.Property(e => e.DocsMedicosValidade).HasColumnType("datetime");

                entity.Property(e => e.PlanoSaudeValidade).HasColumnType("datetime");

                entity.HasOne(e => e.Paciente).WithMany().HasForeignKey(e => e.PacienteId);
                entity.HasOne(e => e.Responsavel).WithMany().HasForeignKey(e => e.ResponsavelId);
                entity.HasOne(e => e.StatusClinico).WithMany().HasForeignKey(e => e.StatusClinicoId);
                entity.HasOne(e => e.TipoAtendimento).WithMany().HasForeignKey(e => e.TipoAtendimentoId);
                entity.HasOne(e => e.HospitalOrigem).WithMany().HasForeignKey(e => e.HospitalOrigemId);
                entity.HasOne(e => e.UnidadeDestino).WithMany().HasForeignKey(e => e.UnidadeDestinoId);
            });

            modelBuilder.Entity<Cidades>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.Cidade).HasMaxLength(255);

                entity.Property(e => e.EstadoId).HasColumnName("EstadoId");

                entity.HasOne(e => e.Estado).WithMany(es => es.CidadesEstados).HasForeignKey(e => e.EstadoId);
            });

            modelBuilder.Entity<EstadoCivil>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<Estados>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.Estado).HasMaxLength(255);

                entity.Property(e => e.Sigla).HasMaxLength(255);
            });

            modelBuilder.Entity<StatusClinico>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.Cor).HasMaxLength(255);

                entity.Property(e => e.Status).HasMaxLength(255);
            });

            modelBuilder.Entity<TiposAtendimento>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.TipoAtendimento).HasMaxLength(255);
            });

            modelBuilder.Entity<TransferenciasPacientes>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.CidadeDestino).HasMaxLength(255);

                entity.Property(e => e.CidadeOrigem).HasMaxLength(255);

                entity.Property(e => e.DataTransferencia).HasColumnType("datetime");

                entity.Property(e => e.HoraChegada).HasColumnType("datetime");

                entity.Property(e => e.HoraSaida)
                    .HasColumnName("_HoraSaida")
                    .HasColumnType("datetime");

                entity.Property(e => e.NivelTransporte).HasMaxLength(255);

                entity.Property(e => e.ServicosAdicionais).HasMaxLength(255);

                entity.Property(e => e.TipoTransporte).HasMaxLength(255);

                entity.HasOne(e => e.Paciente).WithMany().HasForeignKey(e => e.PacienteId);
                entity.HasOne(e => e.Transferencia).WithMany().HasForeignKey(e => e.TransferenciaVinculada);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.Property(e => e.Cpf).HasColumnName("cpf");

                entity.Property(e => e.DataDeNascimento)
                    .HasColumnName("data de nascimento")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.EstadoCivil).HasColumnName("estado civil");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(255);

                entity.Property(e => e.Senha).HasColumnName("senha");

                entity.Property(e => e.Sexo)
                    .HasColumnName("sexo")
                    .HasMaxLength(255);

                entity.Property(e => e.TamanhoFamilia).HasColumnName("tamanhoFamilia");

                entity.Property(e => e.Telefone).HasColumnName("telefone");

                entity.Property(e => e.TipoUsuario).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
