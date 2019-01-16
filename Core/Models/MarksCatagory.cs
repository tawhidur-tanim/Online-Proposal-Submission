using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class MarksCatagory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Mark { get; set; }

        public IList<SemesterCatagory> SemesterCatagories { get; set; }

        public MarksCatagory()
        {
            SemesterCatagories = new List<SemesterCatagory>();
        }
    }
}
