using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConvitePrimeiroAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "convite_primeiro_acesso",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token_hash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    expira_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    utilizado_em = table.Column<DateTime>(type: "datetime2", nullable: true),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_convite_primeiro_acesso", x => x.id);
                    table.ForeignKey(
                        name: "fk_convite_primeiro_acesso_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_convite_primeiro_acesso_token_hash",
                table: "convite_primeiro_acesso",
                column: "token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_convite_primeiro_acesso_usuario_id_utilizado_em",
                table: "convite_primeiro_acesso",
                columns: new[] { "usuario_id", "utilizado_em" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "convite_primeiro_acesso");
        }
    }
}
