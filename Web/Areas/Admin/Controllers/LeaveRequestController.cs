using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LeaveRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveRequestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var leaves = _unitOfWork.LeaveRequest.GetAll(includeProperties: "User,User.Department");

            var dtos = leaves.Select(l => new LeaveRequestDto
            {
                LeaveRequestId = l.LeaveRequestId,
                UserId = l.UserId,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Reason = l.Reason,
                ManagerNote = l.ManagerNote,
                Status = l.Status,
                CreatedAt = l.CreatedAt,
                UserName = l.User.FirstName + " " + l.User.LastName,
                DepartmentName = l.User.Department?.DepartmentName ?? "-"
            }).ToList();

            return View(dtos);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _unitOfWork.LeaveRequest.Get(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<LeaveRequestDto>(entity);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(LeaveRequestDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var entity = _unitOfWork.LeaveRequest.Get(dto.LeaveRequestId);
            if (entity == null) return NotFound();

            // Mapping: sadece ilgili alanlar
            entity.Status = dto.Status;
            entity.ManagerNote = dto.ManagerNote;

            _unitOfWork.LeaveRequest.Update(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.LeaveRequest.Get(id);
            if (entity == null) return NotFound();

            _unitOfWork.LeaveRequest.Remove(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
