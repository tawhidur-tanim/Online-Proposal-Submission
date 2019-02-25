using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public int? SemesterId { get; set; }

        public Semester Semester { get; set; }

        public IEnumerable<Proposal> Proposals { get; set; }

        public ApplicationUser()
        {
            Proposals = new List<Proposal>();
        }
    }
}
