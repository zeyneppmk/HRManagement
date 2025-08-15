using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var payments = _unitOfWork.Payment.GetAll();
            var dtos = _mapper.Map<IEnumerable<PaymentDto>>(payments);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var payment = _unitOfWork.Payment.Get(id);
            if (payment == null) return NotFound();

            var dto = _mapper.Map<PaymentDto>(payment);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PaymentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var entity = _mapper.Map<Payment>(dto);
            _unitOfWork.Payment.Add(entity);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = entity.PaymentId }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PaymentDto dto)
        {
            if (id != dto.PaymentId) return BadRequest();

            var existing = _unitOfWork.Payment.Get(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            _unitOfWork.Payment.Update(existing);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var payment = _unitOfWork.Payment.Get(id);
            if (payment == null) return NotFound();

            _unitOfWork.Payment.Remove(payment);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
