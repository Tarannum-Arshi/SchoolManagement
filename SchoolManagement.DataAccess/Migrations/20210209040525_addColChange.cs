using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.DataAccess.Migrations
{
    public partial class addColChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_UserId",
                table: "Subject",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_UserModel_UserId",
                table: "Subject",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_UserModel_UserId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_UserId",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Subject",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Subject",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
