using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddSomePropertiesToModel3DData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Model3DData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAsset",
                table: "Model3DData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WordAssetID",
                table: "Mode3DBehaviorData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Model3DData");

            migrationBuilder.DropColumn(
                name: "NameAsset",
                table: "Model3DData");

            migrationBuilder.DropColumn(
                name: "WordAssetID",
                table: "Mode3DBehaviorData");
        }
    }
}
