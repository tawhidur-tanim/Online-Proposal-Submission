using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFinal101.Migrations
{
    public partial class SeedProposalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT [dbo].[ProposalType] ON
INSERT INTO [dbo].[ProposalType] ([Id], [Description]) VALUES (1, N'Project')
INSERT INTO [dbo].[ProposalType] ([Id], [Description]) VALUES (2, N'Internship')
INSERT INTO [dbo].[ProposalType] ([Id], [Description]) VALUES (3, N'Thesis')
SET IDENTITY_INSERT [dbo].[ProposalType] OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" delete from ProposalType where Id in (1,2,3)");
        }
    }
}
