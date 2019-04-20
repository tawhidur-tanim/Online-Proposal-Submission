using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleReference.Admin_Teacher)]
    public class UserController : BaseController<UserResource, ApplicationUser, IUserRepository>
    {
        public UserController(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        {
        }


        [HttpGet("sups")]
        public IActionResult SupervisorQuery(string query)
        {
            try
            {
                var sups = Repository.UserSearch(query, RoleReference.Teacher);

                return Ok(sups.Select(Mapper.Map<ApplicationUser, UserResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpPost("assign")]
        public IActionResult AssignTeacher(TeacherAssignResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var student = Repository.FirstOrDefault(x => x.Id == resource.StudentId);

                if (student == null)
                    return NotFound();

                if (resource.Type == 1)
                {
                    student.SupervisorId = resource.TeacherId;
                }
                else if (resource.Type == 2)
                {
                    student.ReviewerId = resource.TeacherId;
                }

                UnitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("seminar")]
        public IActionResult Seminar(string studentId, bool status)
        {
            try
            {
                if (string.IsNullOrEmpty(studentId))
                    return BadRequest();

                var student = Repository.FirstOrDefault(x => x.Id == studentId);

                if (student == null)
                    return NotFound();

                student.IsSeminar = status;
                UnitOfWork.Complete();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("teacher/students")]
        public IActionResult StudentsByTeacher(string teacherId, bool type)
        {
            try
            {
                if (string.IsNullOrEmpty(teacherId))
                    return BadRequest();

                var students = Repository.GetStudentsByTeacher(teacherId, type);

                return Ok(students.Select(Mapper.Map<ApplicationUser, UserWithProposalResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpGet("sup/category/{semesterId}")]
        public IActionResult GetCategory(int semesterId)
        {
            try
            {
                var marksCategories = Repository.GetCategoryByStudent(semesterId, 1);

                return Ok(marksCategories.Select(Mapper.Map<MarksCatagory, MarksCatagoryResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpGet("sup/category/{semesterId}/{studentId}/{type}")]
        public IActionResult GetCategoryWithMarks(int semesterId, string studentId, byte type)
        {
            try
            {
                var marksCategories = Repository.GetCategoryByStudent(semesterId, type);

                var maps = Repository.GetStudentMarks(studentId, marksCategories.Select(x => x.Id))
                    .ToDictionary(x => x.MarksId);

                var totalMarks = Repository.GetStudentTotalMarks(studentId);

                var marksList = marksCategories.Select(catagory => new MarksResource
                {
                    StudentId = studentId,
                    CategoryId = catagory.Id,
                    CategoryMarks = catagory.Mark,
                    CategoryName = catagory.Name,
                    GivenMarks = maps.ContainsKey(catagory.Id) ? maps[catagory.Id].Marks : 0,
                    Remarks = maps.ContainsKey(catagory.Id) ? maps[catagory.Id].Remarks : "",
                })
                .ToList();

                return Ok(new { totalMarks, marksList });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("saveMarks")]
        public IActionResult SaveStudentMarks(IList<MarksResource> marks, [FromQuery]string teacherId)
        {
            try
            {
                var marksMap = marks.Select(mark => new StudentMarkMap
                {
                    EntryDate = DateTime.Now,
                    Marks = mark.GivenMarks,
                    MarksId = mark.CategoryId,
                    StudentId = mark.StudentId,
                    TeacherId = teacherId,
                    Remarks = mark.Remarks
                })
                .ToList();

                Repository.RemoveMarksMap(marks.Select(x => x.StudentId));
                Repository.SaveStudentMarks(marksMap);
                UnitOfWork.Complete();

                return Ok(marksMap);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("passStudent/{studentId}")]
        public IActionResult PassStudent(string studentId)
        {
            try
            {
                var student = Repository.FirstOrDefault(x => x.Id == studentId);

                if (student.SupervisorId != GetUserId())
                    return BadRequest();

                var totalMarks = Repository.GetStudentTotalMarks(studentId);

                if (totalMarks < 40)
                    return BadRequest("Marks Must be at least 40");

                Repository.PassStudent(studentId);
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
