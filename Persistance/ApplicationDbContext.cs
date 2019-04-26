using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Persistance.EntityConfiguration;

namespace ProjectFinal101.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new SemesterConfiguration());
            builder.ApplyConfiguration(new SemesterCatagoryConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ProposalConfiguration());
            builder.ApplyConfiguration(new ProposalTypeConfiguration());
            builder.ApplyConfiguration(new StudentMarkMapConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new StudentCourseGpaMapConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<MarksCatagory> MarksCatagories { get; set; }

        public DbSet<SemesterCatagory> SemesterCatagories { get; set; }

        public DbSet<Proposal> Proposals { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentMarkMap> StudentMarkMaps { get; set; }

        public DbSet<StudentCourseGpaMap> GpaMaps { get; set; }

    }
}
