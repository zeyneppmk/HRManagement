using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HRManagement.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Kullanıcı Listeleme + Arama
        public IActionResult Index(string search, int? departmentId, int? roleId)
        {
            var users = _unitOfWork.User.GetAll(includeProperties: "Role,Department,UserDetail");

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                users = users.Where(u =>
                    (u.FirstName + " " + u.LastName).ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search));
            }

            if (departmentId.HasValue)
            {
                users = users.Where(u => u.DepartmentId == departmentId.Value);
            }

            if (roleId.HasValue)
            {
                users = users.Where(u => u.RoleId == roleId.Value);
            }

            // Dropdownlar için ViewBag
            ViewBag.Departments = _unitOfWork.Department.GetAll()
                .Select(d => new SelectListItem
                {
                    Text = d.DepartmentName,
                    Value = d.DepartmentId.ToString()
                }).ToList();

            ViewBag.Roles = _unitOfWork.Role.GetAll()
                .Select(r => new SelectListItem
                {
                    Text = r.RoleName,
                    Value = r.RoleId.ToString()
                }).ToList();

            var userDtos = _mapper.Map<List<UserDto>>(users);
            var userDetailDtos = _mapper.Map<List<UserDetailDto>>(users.Select(u => u.UserDetail).Where(ud => ud != null));
            ViewBag.UserDetails = userDetailDtos;

            return View(userDtos);
        }
        // GET
        public IActionResult Create()
        {
            LoadRolesAndDepartments();
            return View(new UserDto()); // boş model gönder
        }


        private void LoadRolesAndDepartments()
        {
            ViewBag.Roles = _unitOfWork.Role.GetAll()
                             .Select(x => new SelectListItem
                             {
                                 Text = x.RoleName,
                                 Value = x.RoleId.ToString()
                             }).ToList();

            ViewBag.Departments = _unitOfWork.Department.GetAll()
                                  .Select(x => new SelectListItem
                                  {
                                      Text = x.DepartmentName,
                                      Value = x.DepartmentId.ToString()
                                  }).ToList();
        }


        // GET: Edit
        public IActionResult Edit(int id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == id, includeProperties: "UserDetail,Role,Department");
            if (user == null) return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            var userDetailDto = _mapper.Map<UserDetailDto>(user.UserDetail);

            ViewBag.UserDetail = userDetailDto;

            return View(userDto);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(UserDto userDto, UserDetailDto userDetailDto)
        {
            if (!ModelState.IsValid) return View(userDto);

            var user = _unitOfWork.User.Get(userDto.UserId);
            if (user == null) return NotFound();

            _mapper.Map(userDto, user);
            _unitOfWork.User.Update(user);

            var userDetail = _unitOfWork.UserDetail.Get(userDetailDto.UserDetailId);
            if (userDetail != null)
            {
                _mapper.Map(userDetailDto, userDetail);
                _unitOfWork.UserDetail.Update(userDetail);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.UserId == id, "UserDetail");
            if (user == null) return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            var userDetailDto = _mapper.Map<UserDetailDto>(user.UserDetail);

            ViewBag.UserDetail = userDetailDto;

            return View(userDto);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _unitOfWork.User.Get(id);
            if (user == null) return NotFound();

            var userDetail = _unitOfWork.UserDetail.GetFirstOrDefault(ud => ud.UserId == id);
            if (userDetail != null)
                _unitOfWork.UserDetail.Remove(userDetail);

            _unitOfWork.User.Remove(user);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
