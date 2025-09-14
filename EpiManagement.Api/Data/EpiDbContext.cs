using Microsoft.EntityFrameworkCore;
using EpiManagement.Api.Models;

namespace EpiManagement.Api.Data
{
    public class EpiDbContext : DbContext
    {
        public EpiDbContext(DbContextOptions<EpiDbContext> options) : base(options)
        {
        }

        public DbSet<Epi> Epis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Epi>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CA)
                    .IsRequired();

                entity.Property(e => e.Descricao)
                    .HasMaxLength(500);

                entity.Property(e => e.Validade)
                    .IsRequired()
                    .HasColumnType("DATE");

                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.CA)
                    .IsUnique()
                    .HasDatabaseName("IX_Epi_CA");

                entity.HasIndex(e => e.Nome)
                    .HasDatabaseName("IX_Epi_Nome");

                entity.HasIndex(e => e.Categoria)
                    .HasDatabaseName("IX_Epi_Categoria");
            });

            // Dados de exemplo para desenvolvimento
            modelBuilder.Entity<Epi>().HasData(
                new Epi
                {
                    Id = 1,
                    Nome = "Capacete de Segurança",
                    CA = 12345,
                    Descricao = "Capacete de segurança para proteção da cabeça",
                    Validade = DateTime.Today.AddMonths(6),
                    Categoria = "Proteção da Cabeça"
                },
                new Epi
                {
                    Id = 2,
                    Nome = "Óculos de Proteção",
                    CA = 23456,
                    Descricao = "Óculos de proteção contra impactos",
                    Validade = DateTime.Today.AddDays(15),
                    Categoria = "Proteção dos Olhos"
                },
                new Epi
                {
                    Id = 3,
                    Nome = "Luvas de Segurança",
                    CA = 34567,
                    Descricao = "Luvas de proteção para as mãos",
                    Validade = DateTime.Today.AddDays(-5),
                    Categoria = "Proteção das Mãos"
                },
                new Epi
                {
                    Id = 4,
                    Nome = "Botina de Segurança",
                    CA = 45678,
                    Descricao = "Botina com biqueira de aço",
                    Validade = DateTime.Today.AddMonths(12),
                    Categoria = "Proteção dos Pés"
                }
            );
        }
    }
}

