using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProjectFinal101.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public int? SemesterId { get; set; }

        public Semester Semester { get; set; }

        public string SupervisorId { get; set; }

        public ApplicationUser Supervisor { get; set; }

        public string ReviewerId { get; set; }

        public ApplicationUser Reviewer { get; set; }

        public bool? IsSeminar { get; set; }

        public IEnumerable<Proposal> Proposals { get; set; }

        public IEnumerable<ApplicationUser> SupervisedStudents { get; set; }

        public IEnumerable<ApplicationUser> ReviewStudents { get; set; }

        public ApplicationUser()
        {
            Proposals = new List<Proposal>();
            SupervisedStudents = new List<ApplicationUser>();
            ReviewStudents = new List<ApplicationUser>();
        }
    }
}
