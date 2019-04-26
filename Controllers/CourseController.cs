using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : BaseController<CourseResource, Course, ICourseRepository>
    {
        private readonly IHostingEnvironment _hosting;

        public CourseController(ICourseRepository repository, IMapper mapper,
            IUnitOfWork unitOfWork, IHostingEnvironment hosting)
            : base(repository, mapper, unitOfWork)
        {
            _hosting = hosting;
        }



        [HttpPost("SaveCourses")]
        public async Task<IActionResult> SaveCourse(IFormFile courses)
        {
            try
            {
                var reader = new ExcelReader(_hosting);

                var courseList = await reader.GetCourses(courses);

                Repository.InsertBulkCourse(courseList);

                return Ok(courseList);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }


        [HttpGet("getCourses")]
        [Authorize(Roles = RoleReference.Admin_Teacher_Student)]
        public IActionResult GetCourses()
        {
            try
            {
                var courses = Repository.GetAll().ToList();

                var studentCourses = Repository
                    .GetStudentCourse(GetUserId()).ToDictionary(x => x.CourseId);

                var studentCourseIds = studentCourses
                    .Where(x => Math.Abs(x.Value.Gpa) > 0)
                    .Select(x => x.Key);

                var readOnly = courses
                    .Where(x => studentCourseIds.Contains(x.Id))
                    .Select(x => new CourseResource
                    {
                        Id = x.Id,
                        Title = x.Title,
                        CourseCode = x.CourseCode,
                        Credit = x.Credit,
                        IsReadOnly = true,
                        Gpa = studentCourses[x.Id].Gpa
                    }).ToList();

                var restOfCourse = courses
                    .Where(x => !studentCourseIds.Contains(x.Id))
                    .Select(x => new CourseResource
                    {
                        Id = x.Id,
                        Title = x.Title,
                        CourseCode = x.CourseCode,
                        Credit = x.Credit,
                        Gpa = 0.0
                    })
                    .ToList();

                restOfCourse.AddRange(readOnly);
                return Ok(restOfCourse);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("SaveStudentGpa")]
        [Authorize(Roles = RoleReference.Admin_Teacher_Student)]
        public IActionResult SaveStudentGpa(IList<CourseResource> courses)
        {
            try
            {
                var studetntId = GetUserId();

                var studentGpa = Repository
                    .GetStudentCourse(studetntId)
                    .Select(x => x.CourseId);

                var saveCourses = courses
                    .Where(x => !studentGpa.Contains(x.Id))
                    .Select(x => new StudentCourseGpaMap
                    {
                        CourseId = x.Id,
                        StudentId = studetntId,
                        Gpa = x.Gpa
                    })
                    .ToList();

                Repository.SaveStudentGpa(saveCourses);

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
