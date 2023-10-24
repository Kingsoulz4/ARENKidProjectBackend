using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicAndAddAgeToWordAssetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelAge",
                table: "WordAssetData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model3DDataId",
                table: "WordAssetData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TopicDataDataID",
                table: "WordAssetData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TopicData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ThumbPath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordAssetData_Model3DDataId",
                table: "WordAssetData",
                column: "Model3DDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_WordAssetData_TopicData_Model3DDataId",
                table: "WordAssetData",
                column: "Model3DDataId",
                principalTable: "TopicData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordAssetData_TopicData_Model3DDataId",
                table: "WordAssetData");

            migrationBuilder.DropTable(
                name: "TopicData");

            migrationBuilder.DropIndex(
                name: "IX_WordAssetData_Model3DDataId",
                table: "WordAssetData");

            migrationBuilder.DropColumn(
                name: "LevelAge",
                table: "WordAssetData");

            migrationBuilder.DropColumn(
                name: "Model3DDataId",
                table: "WordAssetData");

            migrationBuilder.DropColumn(
                name: "TopicDataDataID",
                table: "WordAssetData");
        }
    }
}
