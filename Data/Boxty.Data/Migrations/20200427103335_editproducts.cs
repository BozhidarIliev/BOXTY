using Microsoft.EntityFrameworkCore.Migrations;

namespace Boxty.Data.Migrations
{
    public partial class editproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "DECIMAL",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
