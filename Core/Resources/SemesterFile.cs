using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace ProjectFinal101.Core.Resources
{
    public class SemesterFile
    {
        public int MaxBytes { get; set; }

        public string[] FileTypes { get; set; }


        public bool IsValidFile(IFormFile file)
        {
            return file != null && file.Length <= MaxBytes && FileTypes.Any(x => x == Path.GetExtension(file.FileName).ToLower());
        }
    }
}
