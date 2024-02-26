using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1_COMP2139.Migrations
{
    /// <inheritdoc />
    public partial class MinorEditBookDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Book",
                type: "int",
                nullable: true);
        }
    }
}
