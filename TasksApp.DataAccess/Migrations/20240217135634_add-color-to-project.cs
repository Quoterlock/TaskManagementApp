using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addcolortoproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHex",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHex",
                table: "Projects");
        }
    }
}
