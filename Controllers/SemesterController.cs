using AutoMapper;
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
    // [Authorize]
    [ApiController]
    public class SemesterController : BaseController<SemesterCreateResource, Semester, ISemesterRepsitory>
    {
        private readonly ISemesterCatagoryRepository _catagoryRepository;

        public SemesterController(ISemesterRepsitory repsitory,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ISemesterCatagoryRepository catagoryRepository)
            : base(repsitory, mapper, unitOfWork)
        {
            _catagoryRepository = catagoryRepository;
        }


        protected override Semester BeforeCreate(Semester model, SemesterCreateResource resource)
        {

            if (resource.SemesterId > 0)
            {
                var catagories = _catagoryRepository.GetBySemesterId(resource.SemesterId);

                foreach (var catagory in catagories)
                {
                    model.SemesterCatagories.Add(new SemesterCatagory
                    {
                        MarksCatagoryId = catagory.MarksCatagoryId
                    });
                }
            }

            if (resource.SemesterId == -1)
            {
                var sum = resource.Catagories.Aggregate(0, (current, catagory) => current + catagory.Mark);

                if (sum != 100)
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

        // [HttpPost("update")]
        //public IActionResult UpdateSemester(SemesterCreateResource resource)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Fields Required");
        //    }

        //    if (resource.SemesterId == -1)
        //    {
        //        var sum = resource.Catagories.Aggregate(0, (current, catagory) => current + catagory.Mark);

        //        if (sum != 100)
        //        {
        //            ModelState.AddModelError("", "Marsk Must be 100");

        //            return BadRequest(ModelState);
        //        }
        //    }

        //    if (resource.Status == ACTIVE)
        //    {
        //        var activeSemester = Repository.FirstOrDefault(s => s.Status == ACTIVE);
        //        if (activeSemester != null)
        //        {
        //            ModelState.AddModelError("", "There is already a active semester");
        //            return BadRequest(ModelState);
        //        }
        //    }

        //    var semester = Repository.Get(resource.Id);

        //    if (semester == null)
        //    {
        //        return BadRequest("No Semester Found");
        //    }

        //    if (resource.SemesterId > 0 && resource.SemesterId != semester.Parent)
        //    {

        //        var semesterCategories = _catagoryRepository.GetBySemesterId(semester.Id);
        //        _catagoryRepository.RemoveRange(semesterCategories);

        //        var catagories = _catagoryRepository.GetBySemesterId(resource.SemesterId);

        //        foreach (var catagory in catagories)
        //        {
        //            semester.SemesterCatagories.Add(new SemesterCatagory
        //            {
        //                MarksCatagoryId = catagory.MarksCatagoryId
        //            });
        //        }
        //    }
        //}
    }
}
