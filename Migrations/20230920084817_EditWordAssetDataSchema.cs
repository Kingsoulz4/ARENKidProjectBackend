using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class EditWordAssetDataSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model3DData_WordAssetData_WordAssetDataID",
                table: "Model3DData");

            migrationBuilder.DropIndex(
                name: "IX_Model3DData_WordAssetDataID",
                table: "Model3DData");

            migrationBuilder.DropColumn(
                name: "WordAssetDataID",
                table: "Model3DData");

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

            migrationBuilder.CreateIndex(
                name: "IX_Model3DDataWordAssetData_WordAssetDatasID",
                table: "Model3DDataWordAssetData",
                column: "WordAssetDatasID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model3DDataWordAssetData");

            migrationBuilder.AddColumn<long>(
                name: "WordAssetDataID",
                table: "Model3DData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Model3DData_WordAssetDataID",
                table: "Model3DData",
                column: "WordAssetDataID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model3DData_WordAssetData_WordAssetDataID",
                table: "Model3DData",
                column: "WordAssetDataID",
                principalTable: "WordAssetData",
                principalColumn: "ID");
        }
    }
}
