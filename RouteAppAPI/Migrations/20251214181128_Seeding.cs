using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RouteAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DifficultyLevels",
                columns: new[] { "Id", "Description", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 1, "Равен терен, подходящ за начинаещи", "Лесен", 1 },
                    { 2, "Леко неравен терен с малко изкачване", "Умерен", 2 },
                    { 3, "Неравен терен, изисква добра физическа форма", "Среден", 3 },
                    { 4, "Стръмни изкачвания, технически участъци", "Труден", 4 },
                    { 5, "Екстремни условия, за опитни бегачи", "Много труден", 5 }
                });

            migrationBuilder.InsertData(
                table: "RouteTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Започва и завършва на едно и също място", "Кръгов маршрут" },
                    { 2, "От точка А до точка Б", "Линеен маршрут" },
                    { 3, "Маркирани туристически пътеки", "Туристически" },
                    { 4, "Високопланински терен", "Планински" },
                    { 5, "Маршрути през гора", "Горски" },
                    { 6, "Покрай морето или река", "Крайбрежен" }
                });

            migrationBuilder.InsertData(
                table: "TerrainTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Твърда асфалтова настилка", "Асфалт" },
                    { 2, "Чакълест или земен път", "Черен път" },
                    { 3, "Тясна планинска пътека", "Пътека" },
                    { 4, "Скалист терен", "Скали" },
                    { 5, "Тревиста повърхност", "Трева" },
                    { 6, "Комбинация от различни терени", "Смесен" },
                    { 7, "Пясъчна повърхност", "Пясък" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RouteTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RouteTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RouteTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RouteTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RouteTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RouteTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TerrainTypes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
