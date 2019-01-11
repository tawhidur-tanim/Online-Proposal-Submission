using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFinal101.Core.Repositories;
using System.Linq;

namespace ProjectFinal101.Controllers.BaseController
{

    [ApiController]
    public class BaseController<TResource, TModel, TRepository> : ControllerBase
        where TModel : class, new()
        where TRepository : IBaserepository<TModel>
    {
        private readonly TRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(TRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [Authorize]
        [HttpPost("Create")]
        public IActionResult Create(TResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var model = _mapper.Map<TResource, TModel>(resource);

            var modelInDb = _repository.Add(model);

            _unitOfWork.Complete();
            return Ok(_mapper.Map<TModel, TResource>(modelInDb));
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {

            var models = _repository.GetAll();

            return Ok(models.Select(_mapper.Map<TModel, TResource>));
        }

    }
}
