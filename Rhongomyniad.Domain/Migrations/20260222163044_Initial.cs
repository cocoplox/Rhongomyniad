using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rhongomyniad.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "local_games",
                columns: table => new
                {
                    app_id = table.Column<long>(type: "BIGINT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    install_dir = table.Column<string>(type: "TEXT", nullable: false),
                    game_launcher = table.Column<string>(type: "TEXT", nullable: false),
                    save_files_dir = table.Column<string>(type: "TEXT", nullable: true),
                    config_files_dir = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "steam_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    install_path = table.Column<string>(type: "TEXT", nullable: false),
                    library_folders = table.Column<string>(type: "TEXT", nullable: false),
                    last_updated = table.Column<DateTime>(type: "DATETIME", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_steam_settings", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "local_games");

            migrationBuilder.DropTable(
                name: "steam_settings");
        }
    }
}
