using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFlagsToTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsScheduled",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTimeBlocked",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsScheduled",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsTimeBlocked",
                table: "Tasks");
        }
    }
}
