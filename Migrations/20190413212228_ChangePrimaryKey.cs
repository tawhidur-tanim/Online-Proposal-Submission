using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class ChangePrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentMarkMaps",
                table: "StudentMarkMaps");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "StudentMarkMaps",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentMarkMaps",
                table: "StudentMarkMaps",
                columns: new[] { "MarksId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentMarkMaps_TeacherId",
                table: "StudentMarkMaps",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentMarkMaps",
                table: "StudentMarkMaps");

            migrationBuilder.DropIndex(
                name: "IX_StudentMarkMaps_TeacherId",
                table: "StudentMarkMaps");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "StudentMarkMaps",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentMarkMaps",
                table: "StudentMarkMaps",
                columns: new[] { "TeacherId", "StudentId" });
        }
    }
}
