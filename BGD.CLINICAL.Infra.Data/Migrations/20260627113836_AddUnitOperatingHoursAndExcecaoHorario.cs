using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitOperatingHoursAndExcecaoHorario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "excecao_horario",
                table: "agendamento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "horario_funcionamento_unidade",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    unidade_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dia_semana = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    hora_inicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    hora_fim = table.Column<TimeOnly>(type: "time", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_horario_funcionamento_unidade", x => x.id);
                    table.ForeignKey(
                        name: "fk_horario_funcionamento_unidade_empresa_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_horario_funcionamento_unidade_unidade_unidade_id",
                        column: x => x.unidade_id,
                        principalTable: "unidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_horario_funcionamento_unidade_empresa_id_unidade_id_dia_semana",
                table: "horario_funcionamento_unidade",
                columns: new[] { "empresa_id", "unidade_id", "dia_semana" });

            migrationBuilder.CreateIndex(
                name: "ix_horario_funcionamento_unidade_unidade_id",
                table: "horario_funcionamento_unidade",
                column: "unidade_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "horario_funcionamento_unidade");

            migrationBuilder.DropColumn(
                name: "excecao_horario",
                table: "agendamento");
        }
    }
}
