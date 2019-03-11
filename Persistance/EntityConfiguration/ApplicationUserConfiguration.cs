using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFinal101.Core.Models;

namespace ProjectFinal101.Persistance.EntityConfiguration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(x => x.Semester)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.SemesterId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(x => x.Proposals)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SupervisedStudents)
                .WithOne(x => x.Supervisor)
                .HasForeignKey(x => x.SupervisorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ReviewStudents)
                .WithOne(x => x.Reviewer)
                .HasForeignKey(x => x.ReviewerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
