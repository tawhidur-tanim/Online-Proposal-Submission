using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Resources
{
    public class TeacherAssignResource
    {
        [Required]
        public string TeacherId { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required, Range(1, 2)]
        public byte Type { get; set; } // 1 is for supervisor. 2 is for reviewer
    }
}
