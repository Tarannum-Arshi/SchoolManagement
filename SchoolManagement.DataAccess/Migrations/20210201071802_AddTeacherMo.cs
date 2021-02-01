using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.DataAccess.Migrations
{
    public partial class AddTeacherMo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherModel",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherModel", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_TeacherModel_UserModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModel",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassModel",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    FeeCharge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassModel", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_ClassModel_TeacherModel_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherModel",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentModel",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastPaymentMonth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentModel", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentModel_ClassModel_ClassId",
                        column: x => x.ClassId,
                        principalTable: "ClassModel",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentModel_UserModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserModel",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassModel_TeacherId",
                table: "ClassModel",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModel_ClassId",
                table: "StudentModel",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentModel_UserId",
                table: "StudentModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherModel_UserId",
                table: "TeacherModel",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentModel");

            migrationBuilder.DropTable(
                name: "ClassModel");

            migrationBuilder.DropTable(
                name: "TeacherModel");
        }
    }
}
