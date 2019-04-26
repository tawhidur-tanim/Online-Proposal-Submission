namespace ProjectFinal101.Core.Models
{
    public class StudentCourseGpaMap
    {
        public string StudentId { get; set; }

        public int CourseId { get; set; }

        public double Gpa { get; set; }

        public ApplicationUser Student { get; set; }

        public Course Course { get; set; }

    }
}
