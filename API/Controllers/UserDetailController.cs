using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserDetailsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userDetails = _unitOfWork.UserDetail.GetAll();
            var dto = _mapper.Map<IEnumerable<UserDetailDto>>(userDetails);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userDetail = _unitOfWork.UserDetail.Get(id);
            if (userDetail == null) return NotFound();
            var dto = _mapper.Map<UserDetailDto>(userDetail);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDetailDto dto)
        {
            // ID elle girilmişse sıfır değilse reddet
            if (dto.UserDetailId != 0)
            {
                return BadRequest("UserDetailId should not be set when creating a new record.");
            }

            if (!ModelState.IsValid) return BadRequest();

            var userDetail = _mapper.Map<UserDetail>(dto);
            _unitOfWork.UserDetail.Add(userDetail);
            _unitOfWork.Save();

            // EF otomatik ID atar, biz onu geri döneriz
            return CreatedAtAction(nameof(GetById), new { id = userDetail.UserDetailId }, _mapper.Map<UserDetailDto>(userDetail));
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDetailDto dto)
        {
            if (id != dto.UserDetailId) return BadRequest();
            var userDetail = _unitOfWork.UserDetail.Get(id);
            if (userDetail == null) return NotFound();
            _mapper.Map(dto, userDetail);
            _unitOfWork.UserDetail.Update(userDetail);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userDetail = _unitOfWork.UserDetail.Get(id);
            if (userDetail == null) return NotFound();
            _unitOfWork.UserDetail.Remove(userDetail);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
