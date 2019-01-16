using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class Semester
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Status { get; set; }

        public IList<SemesterCatagory> SemesterCatagories { get; set; }

        public Semester()
        {
            SemesterCatagories = new List<SemesterCatagory>();
        }
    }
}
