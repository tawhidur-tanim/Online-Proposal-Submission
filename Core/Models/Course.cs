using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CourseCode { get; set; }

        public int Credit { get; set; }

        public IList<StudentCourseGpaMap> GpaMaps { get; set; }

        public Course()
        {
            GpaMaps = new List<StudentCourseGpaMap>();
        }
    }
}
