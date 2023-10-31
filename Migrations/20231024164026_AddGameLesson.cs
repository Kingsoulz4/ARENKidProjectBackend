using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddGameLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameData_WordAssetData_WordAssetDataID",
                table: "GameData");

            migrationBuilder.DropIndex(
                name: "IX_GameData_WordAssetDataID",
                table: "GameData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "GameData");

            migrationBuilder.CreateTable(
                name: "GameLessonData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameDataID = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: false),
                    WordTeaching = table.Column<string>(type: "TEXT", nullable: true),
                    WordDisturbing = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLessonData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameLessonData_GameData_GameDataID",
                        column: x => x.GameDataID,
                        principalTable: "GameData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameLessonData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLessonData_GameDataID",
                table: "GameLessonData",
                column: "GameDataID");

            migrationBuilder.CreateIndex(
                name: "IX_GameLessonData_WordAssetDataID",
                table: "GameLessonData",
                column: "WordAssetDataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameLessonData");

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "GameData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameData_WordAssetDataID",
                table: "GameData",
                column: "WordAssetDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameData_WordAssetData_WordAssetDataID",
                table: "GameData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");
        }
    }
}
