using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedimentoIdToAgendamentoAndRenameTipoAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "procedimento_id",
                table: "agendamento",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_agendamento_procedimento_id",
                table: "agendamento",
                column: "procedimento_id");

            migrationBuilder.AddForeignKey(
                name: "fk_agendamento_procedimento_procedimento_id",
                table: "agendamento",
                column: "procedimento_id",
                principalTable: "procedimento",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(
                "UPDATE agendamento SET tipo = 'Avaliacao' WHERE tipo IN ('PrimeiraConsulta', 'PRIMEIRA_CONSULTA');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE agendamento SET tipo = 'PrimeiraConsulta' WHERE tipo = 'Avaliacao';");

            migrationBuilder.DropForeignKey(
                name: "fk_agendamento_procedimento_procedimento_id",
                table: "agendamento");

            migrationBuilder.DropIndex(
                name: "ix_agendamento_procedimento_id",
                table: "agendamento");

            migrationBuilder.DropColumn(
                name: "procedimento_id",
                table: "agendamento");
        }
    }
}
