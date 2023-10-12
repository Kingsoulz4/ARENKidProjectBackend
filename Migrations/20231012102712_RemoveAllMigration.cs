using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAllMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimationData",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimationData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AudioData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<long>(type: "INTEGER", nullable: false),
                    AudioType = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioData", x => x.Id);
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
                    ImageType = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Model3DData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LinkDownload = table.Column<string>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model3DData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Link = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    VideoType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoData", x => x.Id);
                });

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
                name: "WordAssets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    LinkDownLoad = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordAssets", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Mode3DBehaviorData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model3DDataID = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Thumb = table.Column<long>(type: "INTEGER", nullable: false),
                    Audio = table.Column<long>(type: "INTEGER", nullable: false),
                    ActionType = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mode3DBehaviorData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mode3DBehaviorData_Model3DData_Model3DDataID",
                        column: x => x.Model3DDataID,
                        principalTable: "Model3DData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Model3DDataWordAssetData",
                columns: table => new
                {
                    Model3DsId = table.Column<long>(type: "INTEGER", nullable: false),
                    WordAssetDatasID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model3DDataWordAssetData", x => new { x.Model3DsId, x.WordAssetDatasID });
                    table.ForeignKey(
                        name: "FK_Model3DDataWordAssetData_Model3DData_Model3DsId",
                        column: x => x.Model3DsId,
                        principalTable: "Model3DData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Model3DDataWordAssetData_WordAssetData_WordAssetDatasID",
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
                name: "IX_GameData_WordAssetDataID",
                table: "GameData",
                column: "WordAssetDataID");

            migrationBuilder.CreateIndex(
                name: "IX_ImageDataWordAssetData_WordAssetDatasID",
                table: "ImageDataWordAssetData",
                column: "WordAssetDatasID");

            migrationBuilder.CreateIndex(
                name: "IX_Mode3DBehaviorData_Model3DDataID",
                table: "Mode3DBehaviorData",
                column: "Model3DDataID");

            migrationBuilder.CreateIndex(
                name: "IX_Model3DDataWordAssetData_WordAssetDatasID",
                table: "Model3DDataWordAssetData",
                column: "WordAssetDatasID");

            migrationBuilder.CreateIndex(
                name: "IX_SyncAudioData_AudioDataId",
                table: "SyncAudioData",
                column: "AudioDataId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoDataWordAssetData_WordAssetDatasID",
                table: "VideoDataWordAssetData",
                column: "WordAssetDatasID");

            migrationBuilder.CreateIndex(
                name: "IX_WordAssetData_WordAssetDataID",
                table: "WordAssetData",
                column: "WordAssetDataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimationDataWordAssetData");

            migrationBuilder.DropTable(
                name: "AudioDataWordAssetData");

            migrationBuilder.DropTable(
                name: "GameData");

            migrationBuilder.DropTable(
                name: "ImageDataWordAssetData");

            migrationBuilder.DropTable(
                name: "Mode3DBehaviorData");

            migrationBuilder.DropTable(
                name: "Model3DDataWordAssetData");

            migrationBuilder.DropTable(
                name: "SyncAudioData");

            migrationBuilder.DropTable(
                name: "VideoDataWordAssetData");

            migrationBuilder.DropTable(
                name: "WordAssets");

            migrationBuilder.DropTable(
                name: "AnimationData");

            migrationBuilder.DropTable(
                name: "ImageData");

            migrationBuilder.DropTable(
                name: "Model3DData");

            migrationBuilder.DropTable(
                name: "AudioData");

            migrationBuilder.DropTable(
                name: "VideoData");

            migrationBuilder.DropTable(
                name: "WordAssetData");
        }
    }
}
