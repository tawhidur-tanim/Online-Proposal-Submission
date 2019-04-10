using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class StudentMarkMapConfiguration : IEntityTypeConfiguration<StudentMarkMap>
    {
        public void Configure(EntityTypeBuilder<StudentMarkMap> builder)
        {
            builder.HasKey(x => new { x.TeacherId, x.StudentId });

            builder.HasOne(x => x.Student)
                .WithMany(x => x.Marks)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.StudentsMark)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
