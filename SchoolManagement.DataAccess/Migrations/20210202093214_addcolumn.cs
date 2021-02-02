using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.DataAccess.Migrations
{
    public partial class addcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentModel_ClassModel_ClassId",
                table: "StudentModel");

            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "StudentModel",
                newName: "Class");

            migrationBuilder.RenameIndex(
                name: "IX_StudentModel_ClassId",
                table: "StudentModel",
                newName: "IX_StudentModel_Class");

            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "ClassModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModel_ClassModel_Class",
                table: "StudentModel",
                column: "Class",
                principalTable: "ClassModel",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentModel_ClassModel_Class",
                table: "StudentModel");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "ClassModel");

            migrationBuilder.RenameColumn(
                name: "Class",
                table: "StudentModel",
                newName: "ClassId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentModel_Class",
                table: "StudentModel",
                newName: "IX_StudentModel_ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentModel_ClassModel_ClassId",
                table: "StudentModel",
                column: "ClassId",
                principalTable: "ClassModel",
                principalColumn: "ClassId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
