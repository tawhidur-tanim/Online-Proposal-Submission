using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class SemesterCatagoryConfiguration : IEntityTypeConfiguration<SemesterCatagory>
    {
        public void Configure(EntityTypeBuilder<SemesterCatagory> builder)
        {
            builder.HasKey(sc => new { sc.MarksCatagoryId, sc.SemesterId });

            builder.HasOne(sc => sc.Semester)
                .WithMany(s => s.SemesterCatagories)
                .HasForeignKey(sc => sc.SemesterId)
                .IsRequired();


            builder.HasOne(sc => sc.MarksCatagory)
                .WithMany(mc => mc.SemesterCatagories)
                .HasForeignKey(sc => sc.MarksCatagoryId)
                .IsRequired();
        }
    }
}
