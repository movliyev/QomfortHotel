using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QomfortHotelFinal.Migrations
{
    public partial class Createtablessss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ServiceeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomService_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomService_Servisees_ServiceeId",
                        column: x => x.ServiceeId,
                        principalTable: "Servisees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomService_RoomId",
                table: "RoomService",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomService_ServiceeId",
                table: "RoomService",
                column: "ServiceeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomService");
        }
    }
}
