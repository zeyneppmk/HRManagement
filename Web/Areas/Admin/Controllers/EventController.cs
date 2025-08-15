using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var events = _unitOfWork.Event.GetAll();
            var dtos = _mapper.Map<List<EventDto>>(events);
            return View(dtos);
        }

        public IActionResult CalendarList()
        {
            var allEvents = _unitOfWork.Event.GetAll().Select(e => _mapper.Map<EventDto>(e)).ToList();
            var generalEvents = allEvents.Where(e => e.Type == EventType.General).ToList();
            var holidayEvents = allEvents.Where(e => e.Type == EventType.Holiday).ToList();

            ViewBag.GeneralEvents = generalEvents;
            ViewBag.HolidayEvents = holidayEvents;
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEvents()
        {
            var events = _unitOfWork.Event.GetAll();
            var dtos = _mapper.Map<List<EventDto>>(events);
            return Json(dtos);
        }
        [HttpPost]
        public IActionResult Create(EventDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var entity = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsFullDay = dto.IsFullDay,
                IsRepeatable = dto.IsRepeatable,
                Type = dto.Type
            };

            _unitOfWork.Event.Add(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var entity = _unitOfWork.Event.Get(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<EventDto>(entity);
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(EventDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var entity = _unitOfWork.Event.Get(dto.EventId);
            if (entity == null) return NotFound();

            _mapper.Map(dto, entity);
            _unitOfWork.Event.Update(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.Event.Get(id);
            if (entity == null) return NotFound();

            _unitOfWork.Event.Remove(entity);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
