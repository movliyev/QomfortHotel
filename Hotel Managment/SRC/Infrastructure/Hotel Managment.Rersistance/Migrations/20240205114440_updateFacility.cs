using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Managment.Rersistance.Migrations
{
    public partial class updateFacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoomFacilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoomFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
