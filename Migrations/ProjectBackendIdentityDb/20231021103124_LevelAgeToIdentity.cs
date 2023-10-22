using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBackend.Migrations.ProjectBackendIdentityDb
{
    /// <inheritdoc />
    public partial class LevelAgeToIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelAge",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelAge",
                table: "AspNetUsers");
        }
    }
}
