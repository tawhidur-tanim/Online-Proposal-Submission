using AutoMapper;
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
                var sups = Repository.SupervisorSearch(query);

                return Ok(sups.Select(Mapper.Map<ApplicationUser,UserResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
