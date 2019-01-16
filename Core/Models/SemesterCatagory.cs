namespace ProjectFinal101.Core.Models
{
    public class SemesterCatagory
    {
        public int SemesterId { get; set; }

        public int MarksCatagoryId { get; set; }

        public Semester Semester { get; set; }

        public MarksCatagory MarksCatagory { get; set; }
    }
}
