using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;
using HRManagement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _unitOfWork.Role.GetAll();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(roleDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _unitOfWork.Role.GetFirstOrDefault(r => r.RoleId == id);
            if (role == null) return NotFound();

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var role = _mapper.Map<Role>(roleDto);
            _unitOfWork.Role.Add(role);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleId }, roleDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleDto roleDto)
        {
            if (id != roleDto.RoleId) return BadRequest();

            var role = _unitOfWork.Role.GetFirstOrDefault(r => r.RoleId == id);
            if (role == null) return NotFound();

            _mapper.Map(roleDto, role);
            _unitOfWork.Role.Update(role);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            var role = _unitOfWork.Role.GetFirstOrDefault(r => r.RoleId == id);
            if (role == null) return NotFound();

            _unitOfWork.Role.Remove(role);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
