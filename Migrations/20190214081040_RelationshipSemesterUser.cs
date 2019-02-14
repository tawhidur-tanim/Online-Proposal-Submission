using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class RelationshipSemesterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SemesterId",
                table: "AspNetUsers",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Semesters_SemesterId",
                table: "AspNetUsers",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Semesters_SemesterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SemesterId",
                table: "AspNetUsers");
        }
    }
}
