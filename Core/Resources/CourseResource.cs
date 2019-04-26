namespace ProjectFinal101.Core.Resources
{
    public class CourseResource
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CourseCode { get; set; }

        public int Credit { get; set; }

        public double Gpa { get; set; }

        public bool IsReadOnly { get; set; } = false;
    }
}
