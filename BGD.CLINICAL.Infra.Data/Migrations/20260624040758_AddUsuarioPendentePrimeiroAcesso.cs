using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioPendentePrimeiroAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "senha_hash",
                table: "usuario",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "pendente_primeiro_acesso",
                table: "usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pendente_primeiro_acesso",
                table: "usuario");

            migrationBuilder.AlterColumn<string>(
                name: "senha_hash",
                table: "usuario",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
