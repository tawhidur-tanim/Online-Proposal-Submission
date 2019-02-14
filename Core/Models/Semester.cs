using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class Semester
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Status { get; set; }

        public int? Parent { get; set; }

        public IList<SemesterCatagory> SemesterCatagories { get; set; }

        public IList<ApplicationUser> Users { get; set; }

        public Semester()
        {
            SemesterCatagories = new List<SemesterCatagory>();
        }

        public void AddMarksCategory(List<SemesterCatagory> catagories)
        {
            foreach (var catagory in catagories)
            {
                SemesterCatagories.Add(new SemesterCatagory
                {
                    MarksCatagoryId = catagory.MarksCatagoryId
                });
            }
        }

    }
}
