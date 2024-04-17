using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1_COMP2139.Migrations
{
    /// <inheritdoc />
    public partial class FlightComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightComments_Flights_FlightId",
                schema: "Identity",
                table: "FlightComments");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                schema: "Identity",
                table: "FlightComments",
                newName: "flightId");

            migrationBuilder.RenameColumn(
                name: "FlightCommentId",
                schema: "Identity",
                table: "FlightComments",
                newName: "flightCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightComments_FlightId",
                schema: "Identity",
                table: "FlightComments",
                newName: "IX_FlightComments_flightId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightComments_Flights_flightId",
                schema: "Identity",
                table: "FlightComments",
                column: "flightId",
                principalSchema: "Identity",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightComments_Flights_flightId",
                schema: "Identity",
                table: "FlightComments");

            migrationBuilder.RenameColumn(
                name: "flightId",
                schema: "Identity",
                table: "FlightComments",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "flightCommentId",
                schema: "Identity",
                table: "FlightComments",
                newName: "FlightCommentId");

            migrationBuilder.RenameIndex(
                name: "IX_FlightComments_flightId",
                schema: "Identity",
                table: "FlightComments",
                newName: "IX_FlightComments_FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightComments_Flights_FlightId",
                schema: "Identity",
                table: "FlightComments",
                column: "FlightId",
                principalSchema: "Identity",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
