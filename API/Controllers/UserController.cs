using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HRManagement.DTOs;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public IActionResult UserGetAll()
        {
            var users = _unitOfWork.User.GetAll();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult UserGetById(int id)
        {
            var user = _unitOfWork.User.Get(id);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult UserCreate([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<User>(userDto);
            _unitOfWork.User.Add(user);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(UserGetById), new { id = user.UserId }, userDto);
        }

        // PUT: api/users
        [HttpPut("{id}")]
        public IActionResult UserUpdate(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId) return BadRequest();

            var user = _unitOfWork.User.Get(id);
            if (user == null) return NotFound();

            _mapper.Map(userDto, user);
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult UserDelete(int id)
        {
            var user = _unitOfWork.User.Get(id);
            if (user == null) return NotFound();

            _unitOfWork.User.Remove(user);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
