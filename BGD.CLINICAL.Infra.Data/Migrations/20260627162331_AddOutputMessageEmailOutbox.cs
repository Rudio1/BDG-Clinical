using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOutputMessageEmailOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "output_message_email",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    unidade_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    destinatario_email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    destinatario_nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    payload_json = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    tentativas = table.Column<int>(type: "int", nullable: false),
                    erro = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    processado_em = table.Column<DateTime>(type: "datetime2", nullable: true),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_output_message_email", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_output_message_email_empresa_id",
                table: "output_message_email",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_output_message_email_status_criado_em",
                table: "output_message_email",
                columns: new[] { "status", "criado_em" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "output_message_email");
        }
    }
}
