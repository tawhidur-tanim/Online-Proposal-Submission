using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Controllers.BaseController;
using ProjectFinal101.Core.Models;
using ProjectFinal101.Core.Repositories;
using ProjectFinal101.Core.Resources;

namespace ProjectFinal101.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : BaseController<SemesterCreateResource, Semester, ISemesterRepsitory>
    {
        public SemesterController(ISemesterRepsitory repsitory, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repsitory, mapper, unitOfWork)
        {

        }
    }
}
