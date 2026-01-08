using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOnce.Teams.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Teams");

            migrationBuilder.CreateTable(
                name: "Constructors",
                schema: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EstablishedYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructorStatistics",
                schema: "Teams",
                columns: table => new
                {
                    ConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructorStatistics", x => x.ConstructorId);
                    table.ForeignKey(
                        name: "FK_ConstructorStatistics_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalSchema: "Teams",
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                schema: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    RacingNumber = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalSchema: "Teams",
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConstructorAllTimeStatistics",
                schema: "Teams",
                columns: table => new
                {
                    ConstructorStatsConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrandPrixEntered = table.Column<int>(type: "int", nullable: false),
                    TeamPoints = table.Column<int>(type: "int", nullable: false),
                    HighestRaceFinish = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Podiums = table.Column<int>(type: "int", nullable: false),
                    HighestGridPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PolePositions = table.Column<int>(type: "int", nullable: false),
                    WorldChampionships = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructorAllTimeStatistics", x => x.ConstructorStatsConstructorId);
                    table.ForeignKey(
                        name: "FK_ConstructorAllTimeStatistics_ConstructorStatistics_ConstructorStatsConstructorId",
                        column: x => x.ConstructorStatsConstructorId,
                        principalSchema: "Teams",
                        principalTable: "ConstructorStatistics",
                        principalColumn: "ConstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConstructorSeasonStatistics",
                schema: "Teams",
                columns: table => new
                {
                    ConstructorStatsConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonPosition = table.Column<int>(type: "int", nullable: false),
                    SeasonPoints = table.Column<int>(type: "int", nullable: false),
                    DhlFastestLaps = table.Column<int>(type: "int", nullable: false),
                    DNFs = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructorSeasonStatistics", x => x.ConstructorStatsConstructorId);
                    table.ForeignKey(
                        name: "FK_ConstructorSeasonStatistics_ConstructorStatistics_ConstructorStatsConstructorId",
                        column: x => x.ConstructorStatsConstructorId,
                        principalSchema: "Teams",
                        principalTable: "ConstructorStatistics",
                        principalColumn: "ConstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverStatistics",
                schema: "Teams",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalRaces = table.Column<int>(type: "int", nullable: false),
                    TotalPodiums = table.Column<int>(type: "int", nullable: false),
                    TotalWins = table.Column<int>(type: "int", nullable: false),
                    TotalPoles = table.Column<int>(type: "int", nullable: false),
                    TotalChampionships = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverStatistics", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_DriverStatistics_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Teams",
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrandPrixStatistics",
                schema: "Teams",
                columns: table => new
                {
                    SeasonStatsConstructorStatsConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrandPrixRaces = table.Column<int>(type: "int", nullable: false),
                    GrandPrixPoints = table.Column<int>(type: "int", nullable: false),
                    GrandPrixWins = table.Column<int>(type: "int", nullable: false),
                    GrandPrixPodiums = table.Column<int>(type: "int", nullable: false),
                    GrandPrixPoles = table.Column<int>(type: "int", nullable: false),
                    GrandPrixTop10s = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrandPrixStatistics", x => x.SeasonStatsConstructorStatsConstructorId);
                    table.ForeignKey(
                        name: "FK_GrandPrixStatistics_ConstructorSeasonStatistics_SeasonStatsConstructorStatsConstructorId",
                        column: x => x.SeasonStatsConstructorStatsConstructorId,
                        principalSchema: "Teams",
                        principalTable: "ConstructorSeasonStatistics",
                        principalColumn: "ConstructorStatsConstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintStatistics",
                schema: "Teams",
                columns: table => new
                {
                    SeasonStatsConstructorStatsConstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintRaces = table.Column<int>(type: "int", nullable: false),
                    SprintPoints = table.Column<int>(type: "int", nullable: false),
                    SprintWins = table.Column<int>(type: "int", nullable: false),
                    SprintPodiums = table.Column<int>(type: "int", nullable: false),
                    SprintPoles = table.Column<int>(type: "int", nullable: false),
                    SprintTop10s = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintStatistics", x => x.SeasonStatsConstructorStatsConstructorId);
                    table.ForeignKey(
                        name: "FK_SprintStatistics_ConstructorSeasonStatistics_SeasonStatsConstructorStatsConstructorId",
                        column: x => x.SeasonStatsConstructorStatsConstructorId,
                        principalSchema: "Teams",
                        principalTable: "ConstructorSeasonStatistics",
                        principalColumn: "ConstructorStatsConstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_ConstructorId",
                schema: "Teams",
                table: "Drivers",
                column: "ConstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructorAllTimeStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "DriverStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "GrandPrixStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "SprintStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "Drivers",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "ConstructorSeasonStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "ConstructorStatistics",
                schema: "Teams");

            migrationBuilder.DropTable(
                name: "Constructors",
                schema: "Teams");
        }
    }
}
