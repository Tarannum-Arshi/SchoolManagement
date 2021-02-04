using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.DataAccess.Migrations
{
    public partial class alterTeacherModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "LeaveDays",
                table: "TeacherModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RemainingLeave",
                table: "TeacherModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "TeacherModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalLeave",
                table: "TeacherModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveDays",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "RemainingLeave",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "TeacherModel");

            migrationBuilder.DropColumn(
                name: "TotalLeave",
                table: "TeacherModel");

        }
    }
}
