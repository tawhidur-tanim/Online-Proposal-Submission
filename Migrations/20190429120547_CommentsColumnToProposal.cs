using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class CommentsColumnToProposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CvPath",
                table: "Proposals",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Proposals",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Proposals");

            migrationBuilder.AlterColumn<string>(
                name: "CvPath",
                table: "Proposals",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
