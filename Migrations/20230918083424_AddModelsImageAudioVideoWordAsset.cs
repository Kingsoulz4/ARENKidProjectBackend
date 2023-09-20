using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsImageAudioVideoWordAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LinkDownload",
                table: "Model3DData",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "Model3DData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WordAssetData",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    PathAsset = table.Column<string>(type: "TEXT", nullable: true),
                    SentenceType = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordAssetData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WordAssetData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AnimationData",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimationData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AnimationData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AudioData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<long>(type: "INTEGER", nullable: false),
                    AudioType = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudioData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "GameData",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ImageData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Link = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    ImageType = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "VideoData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Link = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoType = table.Column<int>(type: "INTEGER", nullable: false),
                    WordAssetDataID = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoData_WordAssetData_WordAssetDataID",
                        column: x => x.WordAssetDataID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SyncAudioData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AudioDataDataID = table.Column<long>(type: "INTEGER", nullable: false),
                    AudioDataId = table.Column<long>(type: "INTEGER", nullable: true),
                    End = table.Column<long>(type: "INTEGER", nullable: false),
                    Start = table.Column<long>(type: "INTEGER", nullable: false),
                    Te = table.Column<long>(type: "INTEGER", nullable: false),
                    Ts = table.Column<long>(type: "INTEGER", nullable: false),
                    Word = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncAudioData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncAudioData_AudioData_AudioDataId",
                        column: x => x.AudioDataId,
                        principalTable: "AudioData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model3DData_WordAssetDataID",
                table: "Model3DData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimationData_WordAssetDataID",
                table: "AnimationData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_AudioData_WordAssetDataID",
                table: "AudioData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_GameData_WordAssetDataID",
                table: "GameData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageData_WordAssetDataID",
                table: "ImageData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_SyncAudioData_AudioDataId",
                table: "SyncAudioData",
                column: "AudioDataId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoData_WordAssetDataID",
                table: "VideoData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_WordAssetData_WordAssetDataID",
                table: "WordAssetData",
                column: "WordAssetDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model3DData_WordAssetData_WordAssetDataID",
                table: "Model3DData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model3DData_WordAssetData_WordAssetDataID",
                table: "Model3DData");

            migrationBuilder.DropTable(
                name: "AnimationData");

            migrationBuilder.DropTable(
                name: "GameData");

            migrationBuilder.DropTable(
                name: "ImageData");

            migrationBuilder.DropTable(
                name: "SyncAudioData");

            migrationBuilder.DropTable(
                name: "VideoData");

            migrationBuilder.DropTable(
                name: "AudioData");

            migrationBuilder.DropTable(
                name: "WordAssetData");

            migrationBuilder.DropIndex(
                name: "IX_Model3DData_WordAssetDataID",
                table: "Model3DData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "Model3DData");

            migrationBuilder.AlterColumn<string>(
                name: "LinkDownload",
                table: "Model3DData",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
