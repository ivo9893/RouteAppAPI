using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RouteAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostGis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutePoints");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<LineString>(
                name: "Path",
                table: "Routes",
                type: "geography(LineString, 4326)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Routes");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "RoutePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RouteId = table.Column<int>(type: "integer", nullable: false),
                    ElevationM = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(10,7)", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(10,7)", nullable: false),
                    SequenceOrder = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutePoints_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_RouteId_SequenceOrder",
                table: "RoutePoints",
                columns: new[] { "RouteId", "SequenceOrder" });
        }
    }
}
