using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QomfortHotelFinal.Migrations
{
    public partial class CreateVcolundbbbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoomentUser",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CommentState",
                table: "Comments",
                newName: "CommentStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentStatus",
                table: "Comments",
                newName: "CommentState");

            migrationBuilder.AddColumn<string>(
                name: "CoomentUser",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
