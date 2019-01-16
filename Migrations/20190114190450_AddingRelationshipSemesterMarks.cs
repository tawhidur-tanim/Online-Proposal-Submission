using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class AddingRelationshipSemesterMarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "MarksCatagories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MarksCatagories_SemesterId",
                table: "MarksCatagories",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksCatagories_Semesters_SemesterId",
                table: "MarksCatagories",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksCatagories_Semesters_SemesterId",
                table: "MarksCatagories");

            migrationBuilder.DropIndex(
                name: "IX_MarksCatagories_SemesterId",
                table: "MarksCatagories");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "MarksCatagories");
        }
    }
}
