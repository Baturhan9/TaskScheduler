using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTechScheduler.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserAdminsId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserAdminsId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IssuedUserAdminId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserAdminsId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AcceptedUserAdminId",
                table: "Tasks",
                column: "AcceptedUserAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AcceptedUserAdminId",
                table: "Tasks",
                column: "AcceptedUserAdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AcceptedUserAdminId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AcceptedUserAdminId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "IssuedUserAdminId",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserAdminsId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserAdminsId",
                table: "Tasks",
                column: "UserAdminsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserAdminsId",
                table: "Tasks",
                column: "UserAdminsId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
