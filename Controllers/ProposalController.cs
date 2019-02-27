using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System.Linq;
using System.Security.Claims;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : BaseController<ProposalResource, Proposal, IProposalRepository>
    {
        public ProposalController(IProposalRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        : base(repository, mapper, unitOfWork)
        {

        }

        protected override Proposal BeforeCreate(Proposal model, ProposalResource resource)
        {
            model.StudentId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            model.Status = ProposalStstus.Pending;


            var proposal =
                Repository.Find(x => x.Status == ProposalStstus.Accepted || x.Status == ProposalStstus.Pending);

            if (proposal.Any())
            {
                Validation = true;
                Message = "Already active or pending proposals remains";
            }

            return model;
        }
    }
}
