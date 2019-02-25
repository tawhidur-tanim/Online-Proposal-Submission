using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class ProposalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<byte>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    AreaOfStudy = table.Column<string>(maxLength: 100, nullable: true),
                    Language = table.Column<string>(maxLength: 100, nullable: true),
                    FrameWorkDescription = table.Column<string>(maxLength: 255, nullable: true),
                    InternshipReason = table.Column<string>(maxLength: 255, nullable: true),
                    IsHaveInternship = table.Column<bool>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 100, nullable: true),
                    CompanyAddress = table.Column<string>(maxLength: 100, nullable: true),
                    JobDescriotion = table.Column<string>(maxLength: 255, nullable: true),
                    InternshipRefernce = table.Column<string>(maxLength: 100, nullable: true),
                    ContactForSupervisor = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proposals");
        }
    }
}
