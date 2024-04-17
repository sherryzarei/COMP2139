using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1_COMP2139.Migrations
{
    /// <inheritdoc />
    public partial class BookUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelComments_Hotels_HotelId",
                schema: "Identity",
                table: "HotelComments");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                schema: "Identity",
                table: "HotelComments",
                newName: "hotelId");

            migrationBuilder.RenameColumn(
                name: "HotelCommentId",
                schema: "Identity",
                table: "HotelComments",
                newName: "hotelCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelComments_HotelId",
                schema: "Identity",
                table: "HotelComments",
                newName: "IX_HotelComments_hotelId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Identity",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Identity",
                table: "Book",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "Identity",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UserId",
                schema: "Identity",
                table: "Book",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_User_UserId",
                schema: "Identity",
                table: "Book",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelComments_Hotels_hotelId",
                schema: "Identity",
                table: "HotelComments",
                column: "hotelId",
                principalSchema: "Identity",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_User_UserId",
                schema: "Identity",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelComments_Hotels_hotelId",
                schema: "Identity",
                table: "HotelComments");

            migrationBuilder.DropIndex(
                name: "IX_Book_UserId",
                schema: "Identity",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Identity",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Identity",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Username",
                schema: "Identity",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "hotelId",
                schema: "Identity",
                table: "HotelComments",
                newName: "HotelId");

            migrationBuilder.RenameColumn(
                name: "hotelCommentId",
                schema: "Identity",
                table: "HotelComments",
                newName: "HotelCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelComments_hotelId",
                schema: "Identity",
                table: "HotelComments",
                newName: "IX_HotelComments_HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelComments_Hotels_HotelId",
                schema: "Identity",
                table: "HotelComments",
                column: "HotelId",
                principalSchema: "Identity",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
