// Areas/Admin/Controllers/DepartmentController.cs
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.Department.GetAll();
            var dtos = _mapper.Map<List<DepartmentDto>>(departments);
            return View(dtos);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DepartmentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Department>(dto);
            _unitOfWork.Department.Add(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _unitOfWork.Department.Get(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<DepartmentDto>(entity);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _unitOfWork.Department.Get(dto.DepartmentId);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _unitOfWork.Department.Update(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.Department.Get(id);
            if (entity == null) return NotFound();

            _unitOfWork.Department.Remove(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
