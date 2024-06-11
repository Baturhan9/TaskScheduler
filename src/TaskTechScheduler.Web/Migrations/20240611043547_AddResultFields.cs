using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTechScheduler.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddResultFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResultDescription",
                table: "Tasks",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultDescription",
                table: "Tasks");
        }
    }
}
