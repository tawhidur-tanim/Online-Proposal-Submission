using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = RoleReference.Admin)]
    [ApiController]
    public class SemesterController : BaseController<SemesterCreateResource, Semester, ISemesterRepsitory>
    {
        private readonly ISemesterCatagoryRepository _catagoryRepository;
        private readonly IHostingEnvironment _host;

        private readonly IUserRepository _userRepository;
        private readonly SemesterFile _file;
        public SemesterController(ISemesterRepsitory repsitory,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ISemesterCatagoryRepository catagoryRepository,
            IHostingEnvironment host,
            IUserRepository userRepository, IOptionsSnapshot<SemesterFile> options)
            : base(repsitory, mapper, unitOfWork)
        {
            _catagoryRepository = catagoryRepository;
            _host = host;

            _file = options.Value;
            _userRepository = userRepository;
        }


        protected override Semester BeforeCreate(Semester model, SemesterCreateResource resource)
        {

            if (resource.SemesterId > 0)
            {
                var catagories = _catagoryRepository.GetBySemesterId(resource.SemesterId);

                model.AddMarksCategory(catagories);
            }

            if (resource.SemesterId == -1)
            {
                if (resource.TotalMarks() != 100)
                {
                    Validation = true;
                    Message = "Marks must be 100";
                }
            }


            if (resource.Status == ACTIVE)
            {
                var activeSemester = Repository.FirstOrDefault(s => s.Status == ACTIVE);
                if (activeSemester != null)
                {
                    Validation = true;
                    Message = "There is already a active semester";
                }
            }

            return model;
        }

        [HttpGet("Semesters")]
        public IActionResult GetSemesters()
        {
            try
            {
                return Ok(Repository.GetWithCategories().Select(Mapper.Map<Semester, SemesterCreateResource>));
            }
            catch (Exception e)
            {
                return BadRequest("Somwthing Gone Wrong");
            }
        }

        [HttpPost("update")]
        public IActionResult UpdateSemester(SemesterCreateResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Fields Required");
                }
                var semester = Repository.GetWithCategory(resource.Id);

                if (semester == null)
                {
                    return BadRequest("No Semester Found");
                }

                if (resource.SemesterId == -1)
                {
                    if (resource.TotalMarks() != 100)
                    {
                        return BadRequest("Marsk Must be 100");
                    }
                    _catagoryRepository.RemoveRange(semester.SemesterCatagories);
                }

                if (resource.Status == ACTIVE)
                {
                    var activeSemester = Repository.FirstOrDefault(s => s.Status == ACTIVE && s.Id != resource.Id);
                    if (activeSemester != null)
                    {
                        return BadRequest("There is already a active semester");
                    }
                }

                if (resource.SemesterId > 0 && resource.SemesterId != semester.Parent)
                {
                    _catagoryRepository.RemoveRange(semester.SemesterCatagories);
                    var catagories = _catagoryRepository.GetBySemesterId(resource.SemesterId);

                    semester.AddMarksCategory(catagories);
                }

                Mapper.Map(resource, semester);

                UnitOfWork.Complete();

                if (resource.SemesterId > 0)
                {
                    Mapper.Map(semester, resource);
                }

                return Ok(resource);
            }
            catch
            {
                return BadRequest("Something Gone Wrong");
            }
        }


        [HttpPost("students")]
        public async Task<IActionResult> UploadStudents([FromForm]FileResource fileResource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not all fields are given");
                }

                var semesterInDb = Repository.Get(fileResource.SemesterId);
                if (semesterInDb == null)
                    return NotFound();

                if (!_file.IsValidFile(fileResource.File))
                    return BadRequest();

                var filePath = await SaveFile(fileResource);

                var students = GetStudents(filePath).ToList();

                var userNames = students.Select(x => x.UserName);

                var studentsInDb = _userRepository.Find(x => userNames.Contains(x.UserName)).Select(x => x.UserName);

                students = students.Where(x => !studentsInDb.Contains(x.UserName)).ToList();

                students.ForEach(x => x.SemesterId = fileResource.SemesterId);

                await _userRepository.InsertBulk(students);
                await _userRepository.AssignRoles(students);

                return Ok(new
                {
                    students,
                    count = students.Count
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("filters")]
        [Authorize(Roles = RoleReference.Admin_Teacher)]
        public IActionResult GetFilterSemesters()
        {
            try
            {
                var semesters = Repository.GetAll();

                return Ok(semesters.Select(x => new FilterResource { Value = x.Id, Text = x.Name }));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //[HttpGet("compare")]
        //public IActionResult Compare()
        //{
        //    var studentsInDb = _userManager.Users.ToList().Select(x => x.UserName);

        //    var path = Path.Combine(Path.Combine(_host.ContentRootPath, "uploads"), "0bfd2d33-07dc-4397-9d0a-3f96400a1d7a.xlsx");

        //    var students = GetStudents(path);

        //    var except = students.Where(x => !studentsInDb.Contains(x.UserName));

        //    return Ok(except);
        //}


        private async Task<string> SaveFile(FileResource fileResource)
        {
            var uploadPath = Path.Combine(_host.ContentRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(fileResource.File.FileName);

            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileResource.File.CopyToAsync(stream);
            }

            return filePath;
        }


        private IList<ApplicationUser> GetStudents(string filePath)
        {
            var workBook = new XLWorkbook(filePath);

            var students = new List<ApplicationUser>();

            foreach (var worksheet in workBook.Worksheets)
            {
                var row = worksheet
                    .FirstRowUsed()
                    .RowUsed();

                row = row.RowBelow();

                while (!row.Cell(1).IsEmpty())
                {
                    var userName = row.Cell(1).GetString();
                    var name = row.Cell(2).GetString();

                    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(name))
                    {

                        if (students.All(x => x.UserName != userName))
                        {
                            var student = new ApplicationUser
                            {
                                UserName = row.Cell(2).GetString().Trim(),
                                FullName = row.Cell(3).GetString().Trim()
                            };

                            students.Add(student);
                        }
                    }


                    row = row.RowBelow();
                }
            }

            return students;
        }

    }
}
