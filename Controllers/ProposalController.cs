using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProposalController : BaseController<ProposalResource, Proposal, IProposalRepository>
    {
        private readonly IHostingEnvironment _hosting;
        private readonly ISemesterRepsitory _semesterRepsitory;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ProposalFile _file;
        public ProposalController(IProposalRepository repository, IMapper mapper, IUnitOfWork unitOfWork,
            IOptionsSnapshot<ProposalFile> snapshot, IHostingEnvironment hosting, ISemesterRepsitory semesterRepsitory, IUserRepository userRepository, ICourseRepository courseRepository)
        : base(repository, mapper, unitOfWork)
        {
            _hosting = hosting;
            _semesterRepsitory = semesterRepsitory;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _file = snapshot.Value;
        }

        protected override Proposal BeforeCreate(Proposal model, ProposalResource resource)
        {
            var userId = GetUserId();

            if (resource.Type == ProposalTypeName.Internship)
            {
                var courses = _courseRepository.GetAll();

                var studentGpa = _courseRepository.GetStudentCourse(userId);

                if (studentGpa.Count != courses.Count())
                {
                    Validation = true;
                    Message = "Please Enter GPA of all required courses";
                }
            }

            model.StudentId = userId;

            model.Status = ProposalStstus.Pending;

            if (!IsStudentCurrentSemeter(userId))
            {
                Validation = true;
                Message = "Invalid Semester";
            }

            var proposal =
                Repository.Find(x => (x.Status == ProposalStstus.Accepted || x.Status == ProposalStstus.Pending) && x.StudentId == userId).ToList();

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

                var path = Path.Combine(_hosting.ContentRootPath, "uploads", Guid.NewGuid() + Path.GetExtension(resource.File.FileName));

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

        private bool IsStudentCurrentSemeter(string userId)
        {
            var activeSemester = _semesterRepsitory.FirstOrDefault(x => x.Status == ACTIVE);

            var user = _userRepository.FirstOrDefault(x => x.Id == userId);

            return user.SemesterId != null && user.SemesterId.Value == activeSemester.Id;
        }

        [HttpGet("truth")]
        public IActionResult GetForm()
        {
            try
            {
                var userId = GetUserId();
                return Ok(new { show = IsStudentCurrentSemeter(userId) || User.IsInRole(RoleReference.Admin) });
            }
            catch
            {
                return Ok(new { show = false });
            }
        }

        [HttpGet("GetProposals")]
        [Authorize(Roles = RoleReference.Admin_Teacher)]
        public IActionResult GetProposals()
        {
            try
            {
                var proposals = Repository.GetProposals();

                return Ok(proposals.Select(Mapper.Map<Proposal, ProposalResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("status/{id}/{statusId}")]
        [Authorize(Roles = RoleReference.Admin_Teacher)]
        public IActionResult ChangeStatus(long id, byte statusId)
        {
            try
            {
                var proposal = Repository.FirstOrDefault(x => x.Id == id);

                if (proposal == null)
                    return NotFound();

                if (statusId < 1 || statusId > 3)
                    return BadRequest();

                proposal.Status = statusId;

                UnitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("excel")]
        [Authorize(Roles = RoleReference.Admin_Teacher)]
        public IActionResult GetExcel([FromBody]ParamResource resource)
        {
            try
            {
                var workBook = new XLWorkbook();

                var sheet = workBook.Worksheets.Add("Proposals"); //.Cell(1, 1).SetValue("Hello World");

                sheet.ColumnWidth = 30;
                sheet.Author = User.Identity.Name;
                sheet.RowHeight = 30;

                sheet.Cell(1, 1).SetValue("Serial").Style.Font.Bold = true;
                sheet.Cell(1, 1).Style.Alignment.Indent = 1;
                sheet.Cell(1, 2).SetValue("Student Name").Style.Font.Bold = true;
                sheet.Cell(1, 3).SetValue("Status").Style.Font.Bold = true;
                sheet.Cell(1, 4).SetValue("Type").Style.Font.Bold = true;
                sheet.Cell(1, 5).SetValue("Supervisor Name").Style.Font.Bold = true;
                sheet.Cell(1, 6).SetValue("Reviewer Name").Style.Font.Bold = true;
                sheet.Cell(1, 7).SetValue("Comments").Style.Font.Bold = true;


                var proposals = Repository.GetProposals(resource);

                var length = proposals.Count;

                for (var i = 2; i <= length + 1; i++)
                {
                    sheet.Cell(i, 1).SetValue(i - 1);
                    sheet.Cell(i, 2).SetValue(proposals[i - 2].Student?.FullName);
                    sheet.Cell(i, 3).SetValue(ProposalStatus[proposals[i - 2].Status]);
                    sheet.Cell(i, 4).SetValue(ProjectTypes[proposals[i - 2].ProposalTypeId]);
                    sheet.Cell(i, 5).SetValue(proposals[i - 2].Student?.Supervisor?.FullName);
                    sheet.Cell(i, 6).SetValue(proposals[i - 2].Student?.Reviewer?.FullName);
                    sheet.Cell(i, 7).SetValue(proposals[i - 2].Comments);
                    sheet.Cell(i, 7).Style.Alignment.WrapText = true;
                }

                sheet.Columns().AdjustToContents();

                var stream = new MemoryStream();
                workBook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HelloWorld.xlsx");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpGet("getCv/{proposalId}")]
        // [Authorize(Roles = RoleReference.Admin_Teacher)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCv(int proposalId)
        {
            try
            {
                if (proposalId <= 0)
                {
                    return BadRequest();
                }

                var proposal = Repository.FirstOrDefault(x => x.Id == proposalId);

                if (string.IsNullOrEmpty(proposal.CvPath))
                    return BadRequest();

                var memoryStream = new MemoryStream();

                using (var fileStream = new FileStream(proposal.CvPath, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);

                }

                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(Path.GetFileName(proposal.CvPath), out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                // memoryStream.Position = 0;
                return File(memoryStream.ToArray(), contentType, Path.GetFileName(proposal.CvPath));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpPost("SaveComments")]
        [Authorize(Roles = RoleReference.Admin_Teacher)]
        public IActionResult SaveComments(CommentsResourse resourse)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();


                var proposal = Repository.FirstOrDefault(x => x.Id == resourse.Id);

                if (proposal == null)
                    return BadRequest();

                proposal.Comments = resourse.Comments;

                UnitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

    }
}
