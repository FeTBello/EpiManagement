using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace EpiManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Epis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CA = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Validade = table.Column<DateTime>(type: "DATE", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epis", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Epis",
                columns: new[] { "Id", "CA", "Categoria", "Descricao", "Nome", "Validade" },
                values: new object[,]
                {
                    { 1, 12345, "Proteção da Cabeça", "Capacete de segurança para proteção da cabeça", "Capacete de Segurança", new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 23456, "Proteção dos Olhos", "Óculos de proteção contra impactos", "Óculos de Proteção", new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 34567, "Proteção das Mãos", "Luvas de proteção para as mãos", "Luvas de Segurança", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, 45678, "Proteção dos Pés", "Botina com biqueira de aço", "Botina de Segurança", new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Epi_CA",
                table: "Epis",
                column: "CA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Epi_Categoria",
                table: "Epis",
                column: "Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Epi_Nome",
                table: "Epis",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Epis");
        }
    }
}
