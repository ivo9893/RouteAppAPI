using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RouteAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class RouteIsDraftColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Routes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Routes");
        }
    }
}
