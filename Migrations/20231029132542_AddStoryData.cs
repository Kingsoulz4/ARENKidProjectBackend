using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddStoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    StoryDataConfigContent = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoryDataWordAssetData",
                columns: table => new
                {
                    StoriesId = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDatasID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryDataWordAssetData", x => new { x.StoriesId, x.WordAssetDatasID });
                    table.ForeignKey(
                        name: "FK_StoryDataWordAssetData_StoryData_StoriesId",
                        column: x => x.StoriesId,
                        principalTable: "StoryData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryDataWordAssetData_WordAssetData_WordAssetDatasID",
                        column: x => x.WordAssetDatasID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryDataWordAssetData_WordAssetDatasID",
                table: "StoryDataWordAssetData",
                column: "WordAssetDatasID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryDataWordAssetData");

            migrationBuilder.DropTable(
                name: "StoryData");
        }
    }
}
