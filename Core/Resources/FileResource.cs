using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Resources
{
    public class FileResource
    {
        [Required]
        public int SemesterId { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}
