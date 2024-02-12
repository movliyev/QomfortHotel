using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QomfortHotelFinal.Migrations
{
    public partial class CreateTablesreletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceeId",
                table: "RoomFacilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacilities_ServiceeId",
                table: "RoomFacilities",
                column: "ServiceeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Servisees_ServiceeId",
                table: "RoomFacilities",
                column: "ServiceeId",
                principalTable: "Servisees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Servisees_ServiceeId",
                table: "RoomFacilities");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacilities_ServiceeId",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "ServiceeId",
                table: "RoomFacilities");
        }
    }
}
