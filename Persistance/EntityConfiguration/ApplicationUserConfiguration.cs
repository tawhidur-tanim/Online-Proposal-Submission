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

        }
    }
}
