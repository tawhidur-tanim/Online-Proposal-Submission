using System;

namespace ProjectFinal101.Core.Models
{
    public class StudentMarkMap
    {
        public string StudentId { get; set; }

        public int MarksId { get; set; }

        public string TeacherId { get; set; }

        public int Marks { get; set; }

        public string Remarks { get; set; }

        public DateTime EntryDate { get; set; }

        public ApplicationUser Student { get; set; }

        public ApplicationUser Teacher { get; set; }
    }
}
