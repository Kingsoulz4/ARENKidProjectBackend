using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateModel3DData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "WordAssets",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Model3DData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LinkDownload = table.Column<string>(type: "TEXT", nullable: false),
                    FileType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model3DData", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_Mode3DBehaviorData_Model3DDataID",
                table: "Mode3DBehaviorData",
                column: "Model3DDataID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mode3DBehaviorData");

            migrationBuilder.DropTable(
                name: "Model3DData");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "WordAssets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
