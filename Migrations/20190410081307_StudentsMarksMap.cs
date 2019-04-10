using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class StudentsMarksMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentMarkMaps",
                columns: table => new
                {
                    StudentId = table.Column<string>(nullable: false),
                    TeacherId = table.Column<string>(nullable: false),
                    MarksId = table.Column<int>(nullable: false),
                    Marks = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMarkMaps", x => new { x.TeacherId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentMarkMaps_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentMarkMaps_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentMarkMaps_StudentId",
                table: "StudentMarkMaps",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentMarkMaps");
        }
    }
}
