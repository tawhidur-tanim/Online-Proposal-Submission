using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectFinal101.Core.Resources
{
    public class SemesterCreateResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte Status { get; set; }

        public int SemesterId { get; set; }

        public IList<MarksCatagoryResource> Catagories { get; set; }

        public SemesterCreateResource()
        {
            Catagories = new List<MarksCatagoryResource>();
        }


        public int TotalMarks()
        {
            return Catagories.Aggregate(0, (current, catagory) => current + catagory.Mark);
        }
    }
}
