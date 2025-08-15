using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var payments = _unitOfWork.Payment.GetAll(includeProperties: "User");
            var dtos = _mapper.Map<List<PaymentDto>>(payments);
            return View(dtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PaymentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<Payment>(dto);
            _unitOfWork.Payment.Add(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _unitOfWork.Payment.Get(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<PaymentDto>(entity);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(PaymentDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _unitOfWork.Payment.Get(dto.PaymentId);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _unitOfWork.Payment.Update(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.Payment.Get(id);
            if (entity == null) return NotFound();

            _unitOfWork.Payment.Remove(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
