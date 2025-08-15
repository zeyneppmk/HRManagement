using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _unitOfWork.TaskItem.GetAll();
            var dto = _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _unitOfWork.TaskItem.Get(id);
            if (task == null) return NotFound();
            var dto = _mapper.Map<TaskItemDto>(task);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var task = _mapper.Map<TaskItem>(dto);
            _unitOfWork.TaskItem.Add(task);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = task.TaskItemId }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TaskItemDto dto)
        {
            if (id != dto.TaskItemId) return BadRequest();
            var task = _unitOfWork.TaskItem.Get(id);
            if (task == null) return NotFound();
            _mapper.Map(dto, task);
            _unitOfWork.TaskItem.Update(task);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _unitOfWork.TaskItem.Get(id);
            if (task == null) return NotFound();
            _unitOfWork.TaskItem.Remove(task);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
