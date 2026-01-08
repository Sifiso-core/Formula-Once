using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOnce.Events.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Events");

            migrationBuilder.CreateTable(
                name: "Circuits",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    LengthKm = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circuits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CircuitLandmarks",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LandmarkType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NearTurn = table.Column<int>(type: "int", nullable: false),
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircuitLandmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CircuitLandmarks_Circuits_CircuitId",
                        column: x => x.CircuitId,
                        principalSchema: "Events",
                        principalTable: "Circuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CircuitLapRecords",
                schema: "Events",
                columns: table => new
                {
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LapTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircuitLapRecords", x => x.CircuitId);
                    table.ForeignKey(
                        name: "FK_CircuitLapRecords_Circuits_CircuitId",
                        column: x => x.CircuitId,
                        principalSchema: "Events",
                        principalTable: "Circuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    CircuitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberOfLaps = table.Column<int>(type: "int", nullable: false),
                    RaceDistanceKm = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_Circuits_CircuitId",
                        column: x => x.CircuitId,
                        principalSchema: "Events",
                        principalTable: "Circuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceSessions",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceSessions_Races_RaceId",
                        column: x => x.RaceId,
                        principalSchema: "Events",
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CircuitLandmarks_CircuitId",
                schema: "Events",
                table: "CircuitLandmarks",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_CircuitId",
                schema: "Events",
                table: "Races",
                column: "CircuitId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSessions_RaceId",
                schema: "Events",
                table: "RaceSessions",
                column: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CircuitLandmarks",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "CircuitLapRecords",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "RaceSessions",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "Races",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "Circuits",
                schema: "Events");
        }
    }
}
