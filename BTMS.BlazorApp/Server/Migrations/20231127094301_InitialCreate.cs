using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTMS.BlazorApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BusType = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusId);
                });

            migrationBuilder.CreateTable(
                name: "BusRoutes",
                columns: table => new
                {
                    BusRouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    To = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ApproximateDistance = table.Column<int>(type: "int", nullable: false),
                    ApproximateJourneyHour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRoutes", x => x.BusRouteId);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    BusScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.BusScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fares",
                columns: table => new
                {
                    FareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusRouteId = table.Column<int>(type: "int", nullable: false),
                    SeatFare = table.Column<decimal>(type: "money", nullable: false),
                    BusType = table.Column<int>(type: "int", nullable: false),
                    FareType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fares", x => x.FareId);
                    table.ForeignKey(
                        name: "FK_Fares_BusRoutes_BusRouteId",
                        column: x => x.BusRouteId,
                        principalTable: "BusRoutes",
                        principalColumn: "BusRouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fares_BusRouteId",
                table: "Fares",
                column: "BusRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_BusId",
                table: "Schedules",
                column: "BusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fares");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "BusRoutes");

            migrationBuilder.DropTable(
                name: "Buses");
        }
    }
}
