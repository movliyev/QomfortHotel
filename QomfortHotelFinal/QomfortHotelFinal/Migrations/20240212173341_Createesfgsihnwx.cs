using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QomfortHotelFinal.Migrations
{
    public partial class Createesfgsihnwx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomService_Rooms_RoomId",
                table: "RoomService");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomService_Servisees_ServiceeId",
                table: "RoomService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomService",
                table: "RoomService");

            migrationBuilder.RenameTable(
                name: "RoomService",
                newName: "RoomServices");

            migrationBuilder.RenameIndex(
                name: "IX_RoomService_ServiceeId",
                table: "RoomServices",
                newName: "IX_RoomServices_ServiceeId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomService_RoomId",
                table: "RoomServices",
                newName: "IX_RoomServices_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomServices",
                table: "RoomServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomServices_Servisees_ServiceeId",
                table: "RoomServices",
                column: "ServiceeId",
                principalTable: "Servisees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Rooms_RoomId",
                table: "RoomServices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomServices_Servisees_ServiceeId",
                table: "RoomServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomServices",
                table: "RoomServices");

            migrationBuilder.RenameTable(
                name: "RoomServices",
                newName: "RoomService");

            migrationBuilder.RenameIndex(
                name: "IX_RoomServices_ServiceeId",
                table: "RoomService",
                newName: "IX_RoomService_ServiceeId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomServices_RoomId",
                table: "RoomService",
                newName: "IX_RoomService_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomService",
                table: "RoomService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomService_Rooms_RoomId",
                table: "RoomService",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomService_Servisees_ServiceeId",
                table: "RoomService",
                column: "ServiceeId",
                principalTable: "Servisees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
