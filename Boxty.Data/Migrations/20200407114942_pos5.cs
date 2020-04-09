using Microsoft.EntityFrameworkCore.Migrations;

namespace Boxty.Data.Migrations
{
    public partial class pos5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableItems_Products_ProductId",
                table: "TableItems");

            migrationBuilder.DropIndex(
                name: "IX_TableItems_ProductId",
                table: "TableItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "TableItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "TableItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProductPrice",
                table: "TableItems",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TableItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "TableItems");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "TableItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TableItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "TableItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_TableItems_ProductId",
                table: "TableItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableItems_Products_ProductId",
                table: "TableItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
