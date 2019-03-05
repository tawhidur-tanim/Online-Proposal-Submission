using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Core.Repositories;
using System;
using System.Linq;
using System.Security.Claims;

namespace ProjectFinal101.Controllers.BaseController
{

    [ApiController]
    public class BaseController<TResource, TModel, TRepository> : ControllerBase
        where TModel : class, new()
        where TRepository : IBaserepository<TModel>
    {
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;
        protected bool Validation;
        protected string Message = "";

        protected readonly int ACTIVE = 1;
        protected readonly int PENDING = 2;
        protected readonly int DISABLED = 3;

        public BaseController(TRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public string GetUserId()
        {
            return User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }


        protected virtual TModel BeforeCreate(TModel model)
        {
            return model;
        }

        protected virtual TModel BeforeCreate(TModel model, TResource resource)
        {
            return model;
        }


        protected virtual void BeforeDelete(int semesterId)
        {
        }

        // [Authorize]
        [HttpPost("Create")]
        public IActionResult Create(TResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var model = Mapper.Map<TResource, TModel>(resource);

                model = BeforeCreate(model);
                model = BeforeCreate(model, resource);

                if (Validation)
                    return BadRequest(Message);

                var modelInDb = Repository.Add(model);

                UnitOfWork.Complete();

                return Ok(Mapper.Map<TModel, TResource>(modelInDb));
            }
            catch (Exception e)
            {
                return BadRequest("Something Gone Wrong");
            }
        }

        //[Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {

            try
            {
                var models = Repository.GetAll();

                return Ok(models.Select(Mapper.Map<TModel, TResource>));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetSingle/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var model = Repository.Get(id);

                return Ok(Mapper.Map<TModel, TResource>(model));
            }
            catch (Exception e)
            {
                return BadRequest("Something Gone Wrong");
            }
        }


        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //BeforeDelete(id);

                var model = Repository.Get(id);
                Repository.Remove(model);

                UnitOfWork.Complete();

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest("Something Gone Wrong");
            }
        }
    }
}
