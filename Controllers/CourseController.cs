using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;
using System;
using System.Threading.Tasks;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
