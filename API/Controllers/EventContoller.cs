using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var events = _unitOfWork.Event.GetAll();
            var dtos = _mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ev = _unitOfWork.Event.Get(id);
            if (ev == null) return NotFound();
            var dto = _mapper.Map<EventDto>(ev);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<Event>(dto);
            _unitOfWork.Event.Add(entity);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = entity.EventId }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EventDto dto)
        {
            if (id != dto.EventId) return BadRequest();
            var entity = _unitOfWork.Event.Get(id);
            if (entity == null) return NotFound();
            _mapper.Map(dto, entity);
            _unitOfWork.Event.Update(entity);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.Event.Get(id);
            if (entity == null) return NotFound();
            _unitOfWork.Event.Remove(entity);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
