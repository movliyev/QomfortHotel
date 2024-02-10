using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QomfortHotelFinal.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_HomeAbouts_HomeAboutId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Servisees_HomeAbouts_HomeAboutId",
                table: "Servisees");

            migrationBuilder.DropIndex(
                name: "IX_Servisees_HomeAboutId",
                table: "Servisees");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HomeAboutId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "HomeAboutId",
                table: "Servisees");

            migrationBuilder.DropColumn(
                name: "HomeAboutId",
                table: "Rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeAboutId",
                table: "Servisees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeAboutId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servisees_HomeAboutId",
                table: "Servisees",
                column: "HomeAboutId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HomeAboutId",
                table: "Rooms",
                column: "HomeAboutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_HomeAbouts_HomeAboutId",
                table: "Rooms",
                column: "HomeAboutId",
                principalTable: "HomeAbouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servisees_HomeAbouts_HomeAboutId",
                table: "Servisees",
                column: "HomeAboutId",
                principalTable: "HomeAbouts",
                principalColumn: "Id");
        }
    }
}
