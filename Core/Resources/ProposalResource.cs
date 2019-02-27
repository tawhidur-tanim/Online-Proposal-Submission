using ProjectFinal101.Core.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Resources
{
    public class ProposalResource
    {
        public long Id { get; set; }

        [Required]
        [Range(1, 3)]
        public byte Status { get; set; }

        [RequiredProposal]
        public byte Type { get; set; }

        [Required]
        public string StudentId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AreaOfStudy { get; set; }

        public string Language { get; set; }

        public string FrameWorkDescription { get; set; }

        public string InternshipReason { get; set; }

        public bool? IsHaveInternship { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string JobDescriotion { get; set; }

        public string InternshipRefernce { get; set; }

        public string ContactForSupervisor { get; set; }
    }
}
