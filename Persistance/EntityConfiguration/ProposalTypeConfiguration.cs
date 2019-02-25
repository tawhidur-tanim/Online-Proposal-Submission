using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class ProposalTypeConfiguration : IEntityTypeConfiguration<ProposalType>
    {
        public void Configure(EntityTypeBuilder<ProposalType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Description)
                .HasMaxLength(255);


            builder.HasMany(x => x.Proposals)
                .WithOne(x => x.ProposalType)
                .HasForeignKey(x => x.ProposalTypeId)
                .IsRequired();
        }
    }
}
