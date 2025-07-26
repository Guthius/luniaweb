using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lunia.V4.Infrastructure.Persistence.Migrations.Stage
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "stage");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "und-u-ks-level2,und-u-ks-level2,icu,False");

            migrationBuilder.CreateTable(
                name: "square_stages",
                schema: "stage",
                columns: table => new
                {
                    server_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    room_number = table.Column<int>(type: "integer", nullable: false),
                    square_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    stage_group_hash = table.Column<int>(type: "integer", nullable: false),
                    access_level = table.Column<int>(type: "integer", nullable: false),
                    connection_count = table.Column<int>(type: "integer", nullable: false),
                    order_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_square_stages", x => new { x.server_name, x.room_number });
                });

            migrationBuilder.CreateTable(
                name: "stage_servers",
                schema: "stage",
                columns: table => new
                {
                    server_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, collation: "case_insensitive"),
                    address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    port = table.Column<int>(type: "integer", nullable: false),
                    room_count = table.Column<int>(type: "integer", nullable: false),
                    is_square = table.Column<bool>(type: "boolean", nullable: false),
                    congestion_level = table.Column<int>(type: "integer", nullable: false),
                    last_up_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stage_servers", x => x.server_name);
                });

            migrationBuilder.InsertData(
                schema: "stage",
                table: "square_stages",
                columns: new[] { "room_number", "server_name", "access_level", "capacity", "connection_count", "order_number", "square_name", "stage_group_hash" },
                values: new object[,]
                {
                    { 0, "Square Server", 0, 70, 0, 0, "Main Square", 53518598 },
                    { 1, "Square Server", 11, 70, 0, 0, "Myth Square", 53518598 },
                    { 2, "Square Server", 12, 70, 0, 0, "PVP Square", 53518598 },
                    { 3, "Square Server", 3, 70, 0, 0, "Beginners Square", 53518598 },
                    { 4, "Square Server", 2, 70, 0, 0, "Slime Racing Square", 53518598 },
                    { 5, "Square Server", 4, 70, 0, 0, "Episode 1 Square", 53518598 },
                    { 6, "Square Server", 5, 70, 0, 0, "Episode 2 Square", 53518598 },
                    { 7, "Square Server", 6, 70, 0, 0, "Episode 3 Square", 53518598 },
                    { 8, "Square Server", 7, 70, 0, 0, "Episode 4 Square", 53518598 },
                    { 9, "Square Server", 8, 70, 0, 0, "Episode 5 Square", 53518598 },
                    { 10, "Square Server", 9, 70, 0, 0, "Episode 6 Square", 53518598 },
                    { 11, "Square Server", 10, 70, 0, 0, "Episode 7 Square", 53518598 }
                });

            migrationBuilder.InsertData(
                schema: "stage",
                table: "stage_servers",
                columns: new[] { "server_name", "address", "congestion_level", "is_square", "last_up_at", "port", "room_count" },
                values: new object[] { "Square Server", "192.168.0.30", 0, true, new DateTimeOffset(new DateTime(2021, 3, 18, 2, 20, 37, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 15554, 50 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "square_stages",
                schema: "stage");

            migrationBuilder.DropTable(
                name: "stage_servers",
                schema: "stage");
        }
    }
}
