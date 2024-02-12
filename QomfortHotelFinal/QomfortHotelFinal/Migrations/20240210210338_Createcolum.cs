using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QomfortHotelFinal.Migrations
{
    public partial class Createcolum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "Reservations");
        }
    }
}
