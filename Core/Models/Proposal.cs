namespace ProjectFinal101.Core.Models
{
    public class Proposal
    {
        public long Id { get; set; }

        public byte Status { get; set; }

        public byte ProposalTypeId { get; set; }

        public ProposalType ProposalType { get; set; }

        public string StudentId { get; set; }

        public ApplicationUser Student { get; set; }

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

        public string CvPath { get; set; }

        public string Comments { get; set; }

    }
}
