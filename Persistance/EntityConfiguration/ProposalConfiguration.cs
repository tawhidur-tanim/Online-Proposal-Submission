using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
    {
        public void Configure(EntityTypeBuilder<Proposal> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.AreaOfStudy)
                .HasMaxLength(100);

            builder.Property(x => x.CompanyAddress)
                .HasMaxLength(100);


            builder.Property(x => x.CompanyName)
                .HasMaxLength(100);

            builder.Property(x => x.ContactForSupervisor)
                .HasMaxLength(255);

            builder.Property(x => x.Description)
                .HasMaxLength(255);

            builder.Property(x => x.FrameWorkDescription)
                .HasMaxLength(255);

            builder.Property(x => x.InternshipReason)
                .HasMaxLength(255);

            builder.Property(x => x.InternshipRefernce)
                .HasMaxLength(100);

            builder.Property(x => x.JobDescriotion)
                .HasMaxLength(255);

            builder.Property(x => x.Language)
                .HasMaxLength(100);
        }
    }
}
