using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddWordDescriptionToModel3D : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WordDescription",
                table: "Model3DData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WordDescription",
                table: "Model3DData");
        }
    }
}
