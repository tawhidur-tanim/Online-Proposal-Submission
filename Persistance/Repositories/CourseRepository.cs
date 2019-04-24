using EFCore.BulkExtensions;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using System.Collections.Generic;

namespace ProjectFinal101.Persistance.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IList<Course> InsertBulkCourse(IList<Course> courses)
        {
            Context.BulkInsert(courses);

            return courses;
        }
    }
}
