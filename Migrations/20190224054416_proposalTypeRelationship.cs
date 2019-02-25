using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class proposalTypeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Proposals",
                newName: "ProposalTypeId");

            migrationBuilder.CreateTable(
                name: "ProposalType",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProposalTypeId",
                table: "Proposals",
                column: "ProposalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_ProposalType_ProposalTypeId",
                table: "Proposals",
                column: "ProposalTypeId",
                principalTable: "ProposalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_ProposalType_ProposalTypeId",
                table: "Proposals");

            migrationBuilder.DropTable(
                name: "ProposalType");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProposalTypeId",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "ProposalTypeId",
                table: "Proposals",
                newName: "Type");
        }
    }
}
