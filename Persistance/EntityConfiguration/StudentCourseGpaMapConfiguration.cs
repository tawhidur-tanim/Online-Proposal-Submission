using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class StudentCourseGpaMapConfiguration : IEntityTypeConfiguration<StudentCourseGpaMap>
    {
        public void Configure(EntityTypeBuilder<StudentCourseGpaMap> builder)
        {
            builder.HasKey(x => new { x.CourseId, x.StudentId });

            builder.HasOne(x => x.Student)
                .WithMany(x => x.GpaMaps)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Course)
                .WithMany(x => x.GpaMaps)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
