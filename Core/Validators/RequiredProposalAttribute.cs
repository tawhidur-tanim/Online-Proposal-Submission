using ProjectFinal101.Core.Resources;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal101.Core.Validators
{
    public class RequiredProposalAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var proposal = (ProposalResource)validationContext.ObjectInstance;

            if (proposal == null)
                return new ValidationResult("");

            if (proposal.Type > ProposalTypeName.Thesis || proposal.Type < ProposalTypeName.Project)
            {
                return new ValidationResult("Type Not Valid");
            }

            if (proposal.Type == ProposalTypeName.Internship && !InternshipVal(proposal))
            {
                return new ValidationResult("Please provide all required fields");
            }

            if ((proposal.Type == ProposalTypeName.Project || proposal.Type == ProposalTypeName.Thesis)
                && !ProposalVal(proposal))
            {
                return new ValidationResult("Please provide all required fields");
            }


            return ValidationResult.Success;
        }


        private bool InternshipVal(ProposalResource resource)
        {

            return !string.IsNullOrEmpty(resource.CompanyAddress) &&
                   !string.IsNullOrEmpty(resource.CompanyName) &&
                   !string.IsNullOrEmpty(resource.ContactForSupervisor) &&
                   !string.IsNullOrEmpty(resource.FrameWorkDescription) &&
                   !string.IsNullOrEmpty(resource.Language) &&
                   !string.IsNullOrEmpty(resource.InternshipReason) &&
                   !string.IsNullOrEmpty(resource.InternshipRefernce) &&
                   !string.IsNullOrEmpty(resource.JobDescriotion);
        }

        private bool ProposalVal(ProposalResource resource)
        {
            return !string.IsNullOrEmpty(resource.AreaOfStudy) &&
                   !string.IsNullOrEmpty(resource.Description) &&
                   !string.IsNullOrEmpty(resource.Title);
        }
    }
}
