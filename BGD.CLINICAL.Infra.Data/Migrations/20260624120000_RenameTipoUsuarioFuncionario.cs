using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations;

/// <inheritdoc />
public partial class RenameTipoUsuarioFuncionario : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            UPDATE usuario
            SET tipo_usuario = 'Funcionario'
            WHERE tipo_usuario IN ('Usuario', 'USUARIO');
            """);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            UPDATE usuario
            SET tipo_usuario = 'Usuario'
            WHERE tipo_usuario IN ('Funcionario', 'FUNCIONARIO');
            """);
    }
}
