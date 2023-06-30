using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicar.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CRM = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Dia = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendas_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgendaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Agendas_AgendaId",
                        column: x => x.AgendaId,
                        principalTable: "Agendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_MedicoId",
                table: "Agendas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_AgendaId",
                table: "Horarios",
                column: "AgendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Agendas");

            migrationBuilder.DropTable(
                name: "Medicos");
        }
    }
}
