using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimationData_WordAssetData_WordAssetDataID",
                table: "AnimationData");

            migrationBuilder.DropForeignKey(
                name: "FK_AudioData_WordAssetData_WordAssetDataID",
                table: "AudioData");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageData_WordAssetData_WordAssetDataID",
                table: "ImageData");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoData_WordAssetData_WordAssetDataID",
                table: "VideoData");

            migrationBuilder.DropIndex(
                name: "IX_VideoData_WordAssetDataID",
                table: "VideoData");

            migrationBuilder.DropIndex(
                name: "IX_ImageData_WordAssetDataID",
                table: "ImageData");

            migrationBuilder.DropIndex(
                name: "IX_AudioData_WordAssetDataID",
                table: "AudioData");

            migrationBuilder.DropIndex(
                name: "IX_AnimationData_WordAssetDataID",
                table: "AnimationData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "VideoData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "ImageData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "AudioData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "AnimationData");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "AudioData",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "AnimationDataWordAssetData",
                columns: table => new
                {
                    AnimationsID = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDatasID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimationDataWordAssetData", x => new { x.AnimationsID, x.WordAssetDatasID });
                    table.ForeignKey(
                        name: "FK_AnimationDataWordAssetData_AnimationData_AnimationsID",
                        column: x => x.AnimationsID,
                        principalTable: "AnimationData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimationDataWordAssetData_WordAssetData_WordAssetDatasID",
                        column: x => x.WordAssetDatasID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AudioDataWordAssetData",
                columns: table => new
                {
                    AudiosId = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDatasID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioDataWordAssetData", x => new { x.AudiosId, x.WordAssetDatasID });
                    table.ForeignKey(
                        name: "FK_AudioDataWordAssetData_AudioData_AudiosId",
                        column: x => x.AudiosId,
                        principalTable: "AudioData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudioDataWordAssetData_WordAssetData_WordAssetDatasID",
                        column: x => x.WordAssetDatasID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageDataWordAssetData",
                columns: table => new
                {
                    ImagesId = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDatasID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDataWordAssetData", x => new { x.ImagesId, x.WordAssetDatasID });
                    table.ForeignKey(
                        name: "FK_ImageDataWordAssetData_ImageData_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "ImageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageDataWordAssetData_WordAssetData_WordAssetDatasID",
                        column: x => x.WordAssetDatasID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoDataWordAssetData",
                columns: table => new
                {
                    VideosId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordAssetDatasID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoDataWordAssetData", x => new { x.VideosId, x.WordAssetDatasID });
                    table.ForeignKey(
                        name: "FK_VideoDataWordAssetData_VideoData_VideosId",
                        column: x => x.VideosId,
                        principalTable: "VideoData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoDataWordAssetData_WordAssetData_WordAssetDatasID",
                        column: x => x.WordAssetDatasID,
                        principalTable: "WordAssetData",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimationDataWordAssetData_WordAssetDatasID",
                table: "AnimationDataWordAssetData",
                column: "WordAssetDatasID");

            migrationBuilder.CreateIndex(
                name: "IX_AudioDataWordAssetData_WordAssetDatasID",
                table: "AudioDataWordAssetData",
                column: "WordAssetDatasID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageDataWordAssetData_WordAssetDatasID",
                table: "ImageDataWordAssetData",
                column: "WordAssetDatasID");

            migrationBuilder.CreateIndex(
                name: "IX_VideoDataWordAssetData_WordAssetDatasID",
                table: "VideoDataWordAssetData",
                column: "WordAssetDatasID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimationDataWordAssetData");

            migrationBuilder.DropTable(
                name: "AudioDataWordAssetData");

            migrationBuilder.DropTable(
                name: "ImageDataWordAssetData");

            migrationBuilder.DropTable(
                name: "VideoDataWordAssetData");

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "VideoData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "ImageData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "AudioData",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "AudioData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "AnimationData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoData_WordAssetDataID",
                table: "VideoData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageData_WordAssetDataID",
                table: "ImageData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_AudioData_WordAssetDataID",
                table: "AudioData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimationData_WordAssetDataID",
                table: "AnimationData",
                column: "WordAssetDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimationData_WordAssetData_WordAssetDataID",
                table: "AnimationData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AudioData_WordAssetData_WordAssetDataID",
                table: "AudioData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageData_WordAssetData_WordAssetDataID",
                table: "ImageData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoData_WordAssetData_WordAssetDataID",
                table: "VideoData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");
        }
    }
}
