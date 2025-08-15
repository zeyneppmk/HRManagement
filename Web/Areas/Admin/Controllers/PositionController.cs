// Areas/Admin/Controllers/PositionController.cs
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PositionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var positions = _unitOfWork.Pozisyon.GetAll(includeProperties: "User");
            var dtos = _mapper.Map<List<PozisyonDto>>(positions);
            return View(dtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PozisyonDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Pozisyon>(dto);
            _unitOfWork.Pozisyon.Add(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _unitOfWork.Pozisyon.Get(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<PozisyonDto>(entity);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(PozisyonDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _unitOfWork.Pozisyon.Get(dto.PozisyonId);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _unitOfWork.Pozisyon.Update(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.Pozisyon.Get(id);
            if (entity == null) return NotFound();

            _unitOfWork.Pozisyon.Remove(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
