using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddVersionToWordAssetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "WordAssetData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "WordAssetData");
        }
    }
}
