using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Managment.Rersistance.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "RoomFacilities");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Servicees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Servicees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Servicees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Servicees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoomFacilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RooImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RooImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RooImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "RooImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RooImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Servicees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Servicees");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Servicees");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Servicees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoomFacilities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RooImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RooImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RooImages");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "RooImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RooImages");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "RoomFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
