using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class AddingColumnsCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");
        }
    }
}
