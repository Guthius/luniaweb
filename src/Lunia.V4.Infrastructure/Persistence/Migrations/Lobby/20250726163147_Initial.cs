using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lunia.V4.Infrastructure.Persistence.Migrations.Lobby
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lobby");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "und-u-ks-level2,und-u-ks-level2,icu,False");

            migrationBuilder.CreateTable(
                name: "accounts",
                schema: "lobby",
                columns: table => new
                {
                    account_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, collation: "case_insensitive"),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    slot_count = table.Column<int>(type: "integer", nullable: false),
                    character_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_logged_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_accounts", x => x.account_name);
                });

            migrationBuilder.CreateTable(
                name: "blocking",
                schema: "lobby",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    account_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, collation: "case_insensitive"),
                    character_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    remote_ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    expires_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blocking", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lobby_servers",
                schema: "lobby",
                columns: table => new
                {
                    server_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, collation: "case_insensitive"),
                    ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    port = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ak_lobby_servers_server_name", x => x.server_name);
                });

            migrationBuilder.CreateTable(
                name: "character_licenses",
                schema: "lobby",
                columns: table => new
                {
                    account_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    class_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_character_licenses", x => new { x.account_name, x.class_number });
                    table.ForeignKey(
                        name: "fk_character_licenses_accounts_account_name",
                        column: x => x.account_name,
                        principalSchema: "lobby",
                        principalTable: "accounts",
                        principalColumn: "account_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "characters",
                schema: "lobby",
                columns: table => new
                {
                    character_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    account_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    virtual_id_code = table.Column<int>(type: "integer", nullable: false),
                    class_number = table.Column<int>(type: "integer", nullable: false),
                    stage_level = table.Column<int>(type: "integer", nullable: false),
                    stage_exp = table.Column<int>(type: "integer", nullable: false),
                    pvp_level = table.Column<int>(type: "integer", nullable: false),
                    pvp_exp = table.Column<int>(type: "integer", nullable: false),
                    war_level = table.Column<int>(type: "integer", nullable: false),
                    war_exp = table.Column<int>(type: "integer", nullable: false),
                    game_money = table.Column<long>(type: "bigint", nullable: false),
                    bank_money = table.Column<long>(type: "bigint", nullable: false),
                    skill_point = table.Column<int>(type: "integer", nullable: false),
                    extra_bag_count = table.Column<int>(type: "integer", nullable: false),
                    extra_bank_bag_count = table.Column<int>(type: "integer", nullable: false),
                    instant_state_flags = table.Column<int>(type: "integer", nullable: false),
                    gm_level = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_logged_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_characters", x => x.character_name);
                    table.ForeignKey(
                        name: "fk_characters_accounts_account_name",
                        column: x => x.account_name,
                        principalSchema: "lobby",
                        principalTable: "accounts",
                        principalColumn: "account_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lobby_connections",
                schema: "lobby",
                columns: table => new
                {
                    server_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, collation: "case_insensitive"),
                    account_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lobby_connections", x => new { x.server_name, x.account_name });
                    table.ForeignKey(
                        name: "fk_lobby_connections_accounts_account_name",
                        column: x => x.account_name,
                        principalSchema: "lobby",
                        principalTable: "accounts",
                        principalColumn: "account_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lobby_connections_lobby_servers_server_name",
                        column: x => x.server_name,
                        principalSchema: "lobby",
                        principalTable: "lobby_servers",
                        principalColumn: "server_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_rebirths",
                schema: "lobby",
                columns: table => new
                {
                    character_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    rebirth_count = table.Column<int>(type: "integer", nullable: false),
                    stored_level = table.Column<int>(type: "integer", nullable: false),
                    skill_point = table.Column<int>(type: "integer", nullable: false),
                    last_rebirth_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    gm_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_character_rebirths", x => x.character_name);
                    table.ForeignKey(
                        name: "fk_character_rebirths_characters_character_name",
                        column: x => x.character_name,
                        principalSchema: "lobby",
                        principalTable: "characters",
                        principalColumn: "character_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
                schema: "lobby",
                columns: table => new
                {
                    character_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bag_number = table.Column<int>(type: "integer", nullable: false),
                    position_number = table.Column<int>(type: "integer", nullable: false),
                    item_hash = table.Column<int>(type: "integer", nullable: false),
                    is_on_bank = table.Column<bool>(type: "boolean", nullable: false),
                    stacked_count = table.Column<int>(type: "integer", nullable: false),
                    item_serial = table.Column<long>(type: "bigint", nullable: true),
                    instance = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_items", x => new { x.character_name, x.bag_number, x.position_number });
                    table.ForeignKey(
                        name: "fk_items_characters_character_name",
                        column: x => x.character_name,
                        principalSchema: "lobby",
                        principalTable: "characters",
                        principalColumn: "character_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "licenses",
                schema: "lobby",
                columns: table => new
                {
                    character_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    stage_group_hash = table.Column<int>(type: "integer", nullable: false),
                    access_level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_licenses", x => new { x.character_name, x.stage_group_hash });
                    table.ForeignKey(
                        name: "fk_licenses_characters_character_name",
                        column: x => x.character_name,
                        principalSchema: "lobby",
                        principalTable: "characters",
                        principalColumn: "character_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "selected_characters",
                schema: "lobby",
                columns: table => new
                {
                    account_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    character_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_selected_characters", x => new { x.account_name, x.character_name });
                    table.ForeignKey(
                        name: "fk_selected_characters_accounts_account_name",
                        column: x => x.account_name,
                        principalSchema: "lobby",
                        principalTable: "accounts",
                        principalColumn: "account_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_selected_characters_characters_character_name",
                        column: x => x.character_name,
                        principalSchema: "lobby",
                        principalTable: "characters",
                        principalColumn: "character_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "lobby",
                table: "accounts",
                columns: new[] { "account_name", "character_count", "created_at", "last_logged_at", "password_hash", "slot_count" },
                values: new object[] { "kaneshaw", 2, new DateTimeOffset(new DateTime(2021, 3, 18, 2, 47, 43, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "a7a59ae9ae908f86ff05250e113eda17", 4 });

            migrationBuilder.InsertData(
                schema: "lobby",
                table: "characters",
                columns: new[] { "character_name", "account_name", "bank_money", "class_number", "created_at", "extra_bag_count", "extra_bank_bag_count", "game_money", "gm_level", "instant_state_flags", "is_deleted", "last_logged_at", "pvp_exp", "pvp_level", "skill_point", "stage_exp", "stage_level", "virtual_id_code", "war_exp", "war_level" },
                values: new object[,]
                {
                    { "Mordio", "kaneshaw", 0L, 13, new DateTimeOffset(new DateTime(2021, 3, 18, 2, 40, 13, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0, 0, 0L, 0, 0, false, null, 0, 1, 6, 0, 99, 2130706434, 0, 1 },
                    { "YamYam", "kaneshaw", 0L, 0, new DateTimeOffset(new DateTime(2021, 3, 18, 2, 5, 35, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 0, 0, 10999999L, 0, 0, false, null, 0, 1, 200, 0, 99, 2130706433, 0, 1 }
                });

            migrationBuilder.InsertData(
                schema: "lobby",
                table: "character_rebirths",
                columns: new[] { "character_name", "gm_level", "last_rebirth_at", "rebirth_count", "skill_point", "stored_level" },
                values: new object[,]
                {
                    { "Mordio", 0, new DateTimeOffset(new DateTime(2020, 3, 18, 2, 42, 52, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 5, 100 },
                    { "YamYam", 0, new DateTimeOffset(new DateTime(2021, 3, 18, 2, 38, 21, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 15, 102 }
                });

            migrationBuilder.InsertData(
                schema: "lobby",
                table: "items",
                columns: new[] { "bag_number", "character_name", "position_number", "instance", "is_on_bank", "item_hash", "item_serial", "stacked_count" },
                values: new object[,]
                {
                    { 1, "YamYam", 0, 100663296L, false, 40470131, 4505093047647042256L, 1 },
                    { 1, "YamYam", 1, 134217728L, false, 31536369, 4505093047647042688L, 1 },
                    { 1, "YamYam", 2, 0L, false, 39720869, 4505093047647041772L, 1 },
                    { 1, "YamYam", 3, 33554432L, false, 30757234, 4505093047647043083L, 1 },
                    { 1, "YamYam", 4, 100663296L, false, 22096338, 4505093047647043489L, 1 },
                    { 1, "YamYam", 5, 0L, false, 30992610, 4505093047647077184L, 1 },
                    { 60, "YamYam", 0, 0L, false, 30957193, 4505093047646365909L, 249 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_characters_account_name",
                schema: "lobby",
                table: "characters",
                column: "account_name");

            migrationBuilder.CreateIndex(
                name: "ix_characters_is_deleted",
                schema: "lobby",
                table: "characters",
                column: "is_deleted");

            migrationBuilder.CreateIndex(
                name: "ix_lobby_connections_account_name",
                schema: "lobby",
                table: "lobby_connections",
                column: "account_name");

            migrationBuilder.CreateIndex(
                name: "ix_selected_characters_character_name",
                schema: "lobby",
                table: "selected_characters",
                column: "character_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blocking",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "character_licenses",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "character_rebirths",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "items",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "licenses",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "lobby_connections",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "selected_characters",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "lobby_servers",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "characters",
                schema: "lobby");

            migrationBuilder.DropTable(
                name: "accounts",
                schema: "lobby");
        }
    }
}
