using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.Linq;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleReference.Admin)]
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
                var marksCategories = Repository.GetCategoryByStudent(semesterId);

                return Ok(marksCategories.Select(Mapper.Map<MarksCatagory, MarksCatagoryResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
