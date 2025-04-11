using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeSaleNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaleNumber",
                table: "Sale",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SaleNumber",
                table: "Sale",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
