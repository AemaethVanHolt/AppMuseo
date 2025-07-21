using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMuseo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDescuentoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desempleado",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "Discapacidad",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "Estudiante",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "FamiliaNumerosa",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "Investigador",
                table: "Descuentos");

            migrationBuilder.DropColumn(
                name: "TerceraEdad",
                table: "Descuentos");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Descuentos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Descuentos");

            migrationBuilder.AddColumn<bool>(
                name: "Desempleado",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Discapacidad",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Estudiante",
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

            migrationBuilder.AddColumn<bool>(
                name: "Investigador",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TerceraEdad",
                table: "Descuentos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
