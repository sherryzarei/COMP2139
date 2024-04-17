using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A1_COMP2139.Migrations
{
    /// <inheritdoc />
    public partial class CarComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarComments",
                schema: "Identity",
                columns: table => new
                {
                    carCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    carId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarComments", x => x.carCommentId);
                    table.ForeignKey(
                        name: "FK_CarComments_Cars_carId",
                        column: x => x.carId,
                        principalSchema: "Identity",
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightComments",
                schema: "Identity",
                columns: table => new
                {
                    FlightCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightComments", x => x.FlightCommentId);
                    table.ForeignKey(
                        name: "FK_FlightComments_Flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "Identity",
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelComments",
                schema: "Identity",
                columns: table => new
                {
                    HotelCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelComments", x => x.HotelCommentId);
                    table.ForeignKey(
                        name: "FK_HotelComments_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "Identity",
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarComments_carId",
                schema: "Identity",
                table: "CarComments",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightComments_FlightId",
                schema: "Identity",
                table: "FlightComments",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelComments_HotelId",
                schema: "Identity",
                table: "HotelComments",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarComments",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "FlightComments",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "HotelComments",
                schema: "Identity");
        }
    }
}
