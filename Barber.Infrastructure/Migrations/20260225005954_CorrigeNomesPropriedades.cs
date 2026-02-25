using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeNomesPropriedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Barbers_BarberId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Barberd",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "startHour",
                table: "Barbershops",
                newName: "StartHour");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "Barbershops",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "endHour",
                table: "Barbershops",
                newName: "EndHour");

            migrationBuilder.AlterColumn<int>(
                name: "BarberId",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Barbers_BarberId",
                table: "Reservations",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Barbers_BarberId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "StartHour",
                table: "Barbershops",
                newName: "startHour");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Barbershops",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "EndHour",
                table: "Barbershops",
                newName: "endHour");

            migrationBuilder.AlterColumn<int>(
                name: "BarberId",
                table: "Reservations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Barberd",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Barbers_BarberId",
                table: "Reservations",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id");
        }
    }
}
