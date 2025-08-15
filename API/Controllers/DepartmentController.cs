using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _unitOfWork.Department.GetAll();
            var dtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var department = _unitOfWork.Department.Get(id);
            if (department == null) return NotFound();

            var dto = _mapper.Map<DepartmentDto>(department);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DepartmentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            // DepartmentId sıfırdan büyükse (istemci gönderdiği için), hata dön
            if (dto.DepartmentId != 0)
                return BadRequest("DepartmentId gönderilmemelidir. Sistem tarafından otomatik atanır.");

            var department = _mapper.Map<Department>(dto);
            _unitOfWork.Department.Add(department);
            _unitOfWork.Save();

            // DTO yerine oluşan entity ile geri dön (ID dahil)
            var createdDto = _mapper.Map<DepartmentDto>(department);
            return CreatedAtAction(nameof(GetById), new { id = department.DepartmentId }, createdDto);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DepartmentDto dto)
        {
            if (id != dto.DepartmentId) return BadRequest();

            var department = _unitOfWork.Department.Get(id);
            if (department == null) return NotFound();

            _mapper.Map(dto, department);
            _unitOfWork.Department.Update(department);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.Department.Get(id);
            if (department == null) return NotFound();

            _unitOfWork.Department.Remove(department);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
