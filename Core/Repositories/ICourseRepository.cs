using ProjectFinal101.Core.Models;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Repositories
{
    public interface ICourseRepository : IBaserepository<Course>
    {
        IList<Course> InsertBulkCourse(IList<Course> courses);
    }
}
