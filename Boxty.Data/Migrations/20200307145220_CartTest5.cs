using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boxty.Data.Migrations
{
    public partial class CartTest5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SentTime",
                table: "OrderDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentTime",
                table: "OrderDetails");
        }
    }
}
