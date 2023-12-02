using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTMS.BlazorApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class PicAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Buses",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Buses");
        }
    }
}
