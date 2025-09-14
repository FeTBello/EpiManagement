using EpiManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace EpiManagement.Api.Migrations
{
    [DbContext(typeof(EpiDbContext))]
    partial class EpiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.9");

            modelBuilder.Entity("EpiManagement.Api.Models.Epi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CA")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Validade")
                        .HasColumnType("DATE");

                    b.HasKey("Id");

                    b.HasIndex("CA")
                        .IsUnique()
                        .HasDatabaseName("IX_Epi_CA");

                    b.HasIndex("Categoria")
                        .HasDatabaseName("IX_Epi_Categoria");

                    b.HasIndex("Nome")
                        .HasDatabaseName("IX_Epi_Nome");

                    b.ToTable("Epis");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CA = 12345,
                            Categoria = "Proteção da Cabeça",
                            Descricao = "Capacete de segurança para proteção da cabeça",
                            Nome = "Capacete de Segurança",
                            Validade = new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 2,
                            CA = 23456,
                            Categoria = "Proteção dos Olhos",
                            Descricao = "Óculos de proteção contra impactos",
                            Nome = "Óculos de Proteção",
                            Validade = new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 3,
                            CA = 34567,
                            Categoria = "Proteção das Mãos",
                            Descricao = "Luvas de proteção para as mãos",
                            Nome = "Luvas de Segurança",
                            Validade = new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            Id = 4,
                            CA = 45678,
                            Categoria = "Proteção dos Pés",
                            Descricao = "Botina com biqueira de aço",
                            Nome = "Botina de Segurança",
                            Validade = new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
