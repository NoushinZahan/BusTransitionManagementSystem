using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTMS.BlazorApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class BusScheduleUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Buses_BusId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "BusRouteId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_BusRouteId",
                table: "Schedules",
                column: "BusRouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_BusRoutes_BusId",
                table: "Schedules",
                column: "BusId",
                principalTable: "BusRoutes",
                principalColumn: "BusRouteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Buses_BusRouteId",
                table: "Schedules",
                column: "BusRouteId",
                principalTable: "Buses",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_BusRoutes_BusId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Buses_BusRouteId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_BusRouteId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "BusRouteId",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Buses_BusId",
                table: "Schedules",
                column: "BusId",
                principalTable: "Buses",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
