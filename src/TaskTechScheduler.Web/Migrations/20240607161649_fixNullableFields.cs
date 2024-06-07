using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTechScheduler.Web.Migrations
{
    /// <inheritdoc />
    public partial class fixNullableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AcceptedUserAdminId",
                table: "Tasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDate",
                table: "Tasks",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "AcceptedUserAdminId",
                table: "Tasks",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AcceptedUserAdminId",
                table: "Tasks",
                column: "AcceptedUserAdminId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AcceptedUserAdminId",
                table: "Tasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDate",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AcceptedUserAdminId",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AcceptedUserAdminId",
                table: "Tasks",
                column: "AcceptedUserAdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
