using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(x => x.CourseCode)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(x => x.Credit);
        }
    }
}
