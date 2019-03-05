using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.IO;
using System.Linq;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalController : BaseController<ProposalResource, Proposal, IProposalRepository>
    {
        private readonly IHostingEnvironment _hosting;
        private readonly ProposalFile _file;
        public ProposalController(IProposalRepository repository, IMapper mapper, IUnitOfWork unitOfWork,
            IOptionsSnapshot<ProposalFile> snapshot, IHostingEnvironment hosting)
        : base(repository, mapper, unitOfWork)
        {
            _hosting = hosting;
            _file = snapshot.Value;
        }

        protected override Proposal BeforeCreate(Proposal model, ProposalResource resource)
        {
            var userId = GetUserId();
            model.StudentId = userId;

            model.Status = ProposalStstus.Pending;


            var proposal =
                Repository.Find(x => x.Status == ProposalStstus.Accepted || x.Status == ProposalStstus.Pending && x.StudentId == userId);

            if (proposal.Any())
            {
                Validation = true;
                Message = "Already active or pending proposals remains";
            }

            return model;
        }

        [HttpGet("Own")]
        public IActionResult GetOwnProposals()
        {
            try
            {
                var userId = GetUserId();
                var proposal = Repository.GetProposalByStudent(userId);

                return Ok(proposal.Select(Mapper.Map<Proposal, ProposalResource>));
            }
            catch (Exception e)
            {
                return BadRequest("Something Gone Wrong");
            }
        }

        [HttpPost("cv")]
        public IActionResult SaveCv([FromForm]FileResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var proposal = Repository.FirstOrDefault(x => x.Id == resource.SemesterId);

                if (proposal == null)
                    return NotFound();

                if (!_file.IsValidFile(resource.File))
                    return BadRequest();

                var path = Path.Combine(_hosting.ContentRootPath, "uploads", Guid.NewGuid() + Path.GetExtension(resource.File.Name));

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    resource.File.CopyTo(stream);
                }

                proposal.CvPath = path;
                UnitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
