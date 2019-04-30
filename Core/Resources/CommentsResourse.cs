using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Resources
{
    public class CommentsResourse
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Comments { get; set; }
    }
}
