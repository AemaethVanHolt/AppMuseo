using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMuseo.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndEntryProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccesoPreferente",
                table: "Extras",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Extras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "GuiaEnLenguaExtranjera",
                table: "Extras",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Extras",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tienda",
                table: "Extras",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncluyeAccesoPreferente",
                table: "Entradas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncluyeGuiaExtranjera",
                table: "Entradas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncluyeParking",
                table: "Entradas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncluyeTienda",
                table: "Entradas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Desempleado",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FamiliaNumerosa",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Descuentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "TerceraEdad",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Biografia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Intereses",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccesoPreferente",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "GuiaEnLenguaExtranjera",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "Tienda",
                table: "Extras");

            migrationBuilder.DropColumn(
                name: "IncluyeAccesoPreferente",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "IncluyeGuiaExtranjera",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "IncluyeParking",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "IncluyeTienda",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Desempleado",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "FamiliaNumerosa",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "TerceraEdad",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "Biografia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Intereses",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "AspNetUsers");
        }
    }
}
