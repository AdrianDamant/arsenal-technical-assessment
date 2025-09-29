using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsenalTechnicalAssignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAdditionalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballClubs",
                columns: table => new
                {
                    FootballClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballClubs", x => x.FootballClubId);
                });

            migrationBuilder.CreateTable(
                name: "Fixtures",
                columns: table => new
                {
                    FixtureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FixtureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeFootballClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayFootballClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixtures", x => x.FixtureId);
                    table.ForeignKey(
                        name: "FK_Fixtures_FootballClubs_AwayFootballClubId",
                        column: x => x.AwayFootballClubId,
                        principalTable: "FootballClubs",
                        principalColumn: "FootballClubId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Fixtures_FootballClubs_HomeFootballClubId",
                        column: x => x.HomeFootballClubId,
                        principalTable: "FootballClubs",
                        principalColumn: "FootballClubId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FixtureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeTeamGoals = table.Column<int>(type: "int", nullable: false),
                    AwayTeamGoals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Fixtures_FixtureId",
                        column: x => x.FixtureId,
                        principalTable: "Fixtures",
                        principalColumn: "FixtureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_AwayFootballClubId",
                table: "Fixtures",
                column: "AwayFootballClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_HomeFootballClubId",
                table: "Fixtures",
                column: "HomeFootballClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_FixtureId",
                table: "Results",
                column: "FixtureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Fixtures");

            migrationBuilder.DropTable(
                name: "FootballClubs");
        }
    }
}
