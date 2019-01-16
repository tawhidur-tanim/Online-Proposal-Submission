using System.Collections.Generic;

namespace ProjectFinal101.Core.Resources
{
    public class SemesterCreateResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte Status { get; set; }

        public int SemesterId { get; set; }

        public IList<MarksCatagoryResource> Catagories { get; set; }

        public SemesterCreateResource()
        {
            Catagories = new List<MarksCatagoryResource>();
        }
    }
}
