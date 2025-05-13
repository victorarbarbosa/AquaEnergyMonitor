using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaEnergyMonitor.Migrations
{
    /// <inheritdoc />
    public partial class ForthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumoAgua_Usuarios_Id",
                table: "ConsumoAgua");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumoEnergia_Usuarios_Id",
                table: "ConsumoEnergia");

            migrationBuilder.AlterColumn<double>(
                name: "ConsumoKiloWatts",
                table: "ConsumoEnergia",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "ConsumoEnergia",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "ConsumoAgua",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoEnergia_UsuarioId",
                table: "ConsumoEnergia",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumoAgua_UsuarioId",
                table: "ConsumoAgua",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumoAgua_Usuarios_UsuarioId",
                table: "ConsumoAgua",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumoEnergia_Usuarios_UsuarioId",
                table: "ConsumoEnergia",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumoAgua_Usuarios_UsuarioId",
                table: "ConsumoAgua");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumoEnergia_Usuarios_UsuarioId",
                table: "ConsumoEnergia");

            migrationBuilder.DropIndex(
                name: "IX_ConsumoEnergia_UsuarioId",
                table: "ConsumoEnergia");

            migrationBuilder.DropIndex(
                name: "IX_ConsumoAgua_UsuarioId",
                table: "ConsumoAgua");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ConsumoEnergia");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "ConsumoAgua");

            migrationBuilder.AlterColumn<decimal>(
                name: "ConsumoKiloWatts",
                table: "ConsumoEnergia",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumoAgua_Usuarios_Id",
                table: "ConsumoAgua",
                column: "Id",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumoEnergia_Usuarios_Id",
                table: "ConsumoEnergia",
                column: "Id",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
