using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechProduct.Migrations
{
    public partial class TwoColumnImageDataAndMimType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "dbProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimType",
                table: "dbProducts",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "dbProducts");

            migrationBuilder.DropColumn(
                name: "ImageMimType",
                table: "dbProducts");
        }
    }
}
