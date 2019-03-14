using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.Linq;

namespace ProjectFinal101.Controllers.BaseController
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
                var sups = Repository.UserSearch(query, RoleReference.Student);

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
    }
}
