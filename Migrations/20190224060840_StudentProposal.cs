using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class StudentProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Proposals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_StudentId",
                table: "Proposals",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_AspNetUsers_StudentId",
                table: "Proposals",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_AspNetUsers_StudentId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_StudentId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Proposals");
        }
    }
}
