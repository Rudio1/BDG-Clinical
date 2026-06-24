using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGD.CLINICAL.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFuncionarioVinculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "funcionario_vinculo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    funcionario_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empresa_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    unidade_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    cargo_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    flag_aplicador = table.Column<bool>(type: "bit", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_funcionario_vinculo", x => x.id);
                    table.CheckConstraint("ck_funcionario_vinculo_empresa_xor_unidade", "([empresa_id] IS NOT NULL AND [unidade_id] IS NULL) OR ([empresa_id] IS NULL AND [unidade_id] IS NOT NULL)");
                    table.ForeignKey(
                        name: "fk_funcionario_vinculo_cargo_cargo_id",
                        column: x => x.cargo_id,
                        principalTable: "cargo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_funcionario_vinculo_empresa_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresa",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_funcionario_vinculo_funcionario_funcionario_id",
                        column: x => x.funcionario_id,
                        principalTable: "funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_funcionario_vinculo_unidade_unidade_id",
                        column: x => x.unidade_id,
                        principalTable: "unidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("""
                INSERT INTO funcionario_vinculo (
                    id, funcionario_id, empresa_id, unidade_id, cargo_id,
                    flag_aplicador, ativo, criado_em, atualizado_em)
                SELECT
                    NEWID(), id, NULL, unidade_id, cargo_id,
                    flag_aplicador, ativo, criado_em, atualizado_em
                FROM funcionario
                WHERE unidade_id IS NOT NULL
                """);

            migrationBuilder.DropForeignKey(
                name: "fk_funcionario_cargo_cargo_id",
                table: "funcionario");

            migrationBuilder.DropForeignKey(
                name: "fk_funcionario_empresa_empresa_id",
                table: "funcionario");

            migrationBuilder.DropForeignKey(
                name: "fk_funcionario_unidade_unidade_id",
                table: "funcionario");

            migrationBuilder.DropIndex(
                name: "ix_funcionario_cargo_id",
                table: "funcionario");

            migrationBuilder.DropIndex(
                name: "ix_funcionario_empresa_id_nome",
                table: "funcionario");

            migrationBuilder.DropIndex(
                name: "ix_funcionario_unidade_id",
                table: "funcionario");

            migrationBuilder.DropColumn(
                name: "cargo_id",
                table: "funcionario");

            migrationBuilder.DropColumn(
                name: "empresa_id",
                table: "funcionario");

            migrationBuilder.DropColumn(
                name: "flag_aplicador",
                table: "funcionario");

            migrationBuilder.DropColumn(
                name: "unidade_id",
                table: "funcionario");

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_vinculo_cargo_id",
                table: "funcionario_vinculo",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_vinculo_empresa_id",
                table: "funcionario_vinculo",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_vinculo_funcionario_id_empresa_id",
                table: "funcionario_vinculo",
                columns: new[] { "funcionario_id", "empresa_id" },
                unique: true,
                filter: "[empresa_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_vinculo_funcionario_id_unidade_id",
                table: "funcionario_vinculo",
                columns: new[] { "funcionario_id", "unidade_id" },
                unique: true,
                filter: "[unidade_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_vinculo_unidade_id",
                table: "funcionario_vinculo",
                column: "unidade_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "funcionario_vinculo");

            migrationBuilder.AddColumn<Guid>(
                name: "cargo_id",
                table: "funcionario",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "empresa_id",
                table: "funcionario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "flag_aplicador",
                table: "funcionario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "unidade_id",
                table: "funcionario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_cargo_id",
                table: "funcionario",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_empresa_id_nome",
                table: "funcionario",
                columns: new[] { "empresa_id", "nome" });

            migrationBuilder.CreateIndex(
                name: "ix_funcionario_unidade_id",
                table: "funcionario",
                column: "unidade_id");

            migrationBuilder.AddForeignKey(
                name: "fk_funcionario_cargo_cargo_id",
                table: "funcionario",
                column: "cargo_id",
                principalTable: "cargo",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_funcionario_empresa_empresa_id",
                table: "funcionario",
                column: "empresa_id",
                principalTable: "empresa",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_funcionario_unidade_unidade_id",
                table: "funcionario",
                column: "unidade_id",
                principalTable: "unidade",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
