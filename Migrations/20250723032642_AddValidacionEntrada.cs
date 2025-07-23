using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMuseo.Migrations
{
    /// <inheritdoc />
    public partial class AddValidacionEntrada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaUso",
                table: "Entradas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Utilizada",
                table: "Entradas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaUso",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Utilizada",
                table: "Entradas");
        }
    }
}
