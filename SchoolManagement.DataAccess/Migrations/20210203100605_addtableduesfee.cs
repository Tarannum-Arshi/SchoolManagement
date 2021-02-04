using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolManagement.DataAccess.Migrations
{
    public partial class addtableduesfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentModel",
                newName: "inStudentId");

            migrationBuilder.CreateTable(
                name: "DuesFee",
                columns: table => new
                {
                    DuesFeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Fee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuesFee", x => x.DuesFeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DuesFee");

            migrationBuilder.RenameColumn(
                name: "inStudentId",
                table: "StudentModel",
                newName: "StudentId");
        }
    }
}
