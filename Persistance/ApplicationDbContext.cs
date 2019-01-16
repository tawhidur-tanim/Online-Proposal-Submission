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

            base.OnModelCreating(builder);
        }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<MarksCatagory> MarksCatagories { get; set; }

        public DbSet<SemesterCatagory> SemesterCatagories { get; set; }
    }
}
