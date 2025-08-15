using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveRequestsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var leaveRequests = _unitOfWork.LeaveRequest.GetAll();
            var dtos = _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var leaveRequest = _unitOfWork.LeaveRequest.Get(id);
            if (leaveRequest == null) return NotFound();
            var dto = _mapper.Map<LeaveRequestDto>(leaveRequest);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] LeaveRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<LeaveRequest>(dto);
            _unitOfWork.LeaveRequest.Add(entity);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = entity.LeaveRequestId }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LeaveRequestDto dto)
        {
            if (id != dto.LeaveRequestId) return BadRequest();
            var entity = _unitOfWork.LeaveRequest.Get(id);
            if (entity == null) return NotFound();
            _mapper.Map(dto, entity);
            _unitOfWork.LeaveRequest.Update(entity);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.LeaveRequest.Get(id);
            if (entity == null) return NotFound();
            _unitOfWork.LeaveRequest.Remove(entity);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
