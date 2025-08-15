using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PozisyonsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PozisyonsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pozisyonlar = _unitOfWork.Pozisyon.GetAll();
            var dto = _mapper.Map<IEnumerable<PozisyonDto>>(pozisyonlar);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pozisyon = _unitOfWork.Pozisyon.Get(id);
            if (pozisyon == null) return NotFound();
            var dto = _mapper.Map<PozisyonDto>(pozisyon);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] PozisyonDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var pozisyon = _mapper.Map<Pozisyon>(dto);
            _unitOfWork.Pozisyon.Add(pozisyon);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = pozisyon.PozisyonId }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PozisyonDto dto)
        {
            if (id != dto.PozisyonId) return BadRequest();
            var pozisyon = _unitOfWork.Pozisyon.Get(id);
            if (pozisyon == null) return NotFound();
            _mapper.Map(dto, pozisyon);
            _unitOfWork.Pozisyon.Update(pozisyon);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pozisyon = _unitOfWork.Pozisyon.Get(id);
            if (pozisyon == null) return NotFound();
            _unitOfWork.Pozisyon.Remove(pozisyon);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
