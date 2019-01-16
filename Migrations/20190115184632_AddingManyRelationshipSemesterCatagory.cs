using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class AddingManyRelationshipSemesterCatagory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SemesterCatagories",
                columns: table => new
                {
                    SemesterId = table.Column<int>(nullable: false),
                    MarksCatagoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterCatagories", x => new { x.MarksCatagoryId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_SemesterCatagories_MarksCatagories_MarksCatagoryId",
                        column: x => x.MarksCatagoryId,
                        principalTable: "MarksCatagories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterCatagories_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterCatagories_SemesterId",
                table: "SemesterCatagories",
                column: "SemesterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterCatagories");
        }
    }
}
