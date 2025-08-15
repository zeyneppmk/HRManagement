

using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HRManagement.Web.Hubs;

namespace HRManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var notifications = _unitOfWork.AppNotification.GetAll(includeProperties: "Sender,Receiver");
            var dtoList = _mapper.Map<IEnumerable<AppNotificationDto>>(notifications);
            return View(dtoList);

        }

        [HttpGet]
        public IActionResult GetAllForDropdown()
        {
            var notifications = _unitOfWork.AppNotification.GetAll(
                includeProperties: "Sender"
            )
            .OrderByDescending(x => x.CreatedAt)
            .Take(10); // Son 10 bildirim

            var dtoList = _mapper.Map<IEnumerable<AppNotificationDto>>(notifications);
            return Json(dtoList);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AppNotificationDto dto)
        {
            var entity = _mapper.Map<AppNotification>(dto);
            _unitOfWork.AppNotification.Add(entity);
            _unitOfWork.Save();

            // Real-time bildirim gönderimi
            await _hubContext.Clients.User(dto.ReceiverId.ToString())
                .SendAsync("ReceiveNotification", dto);

            return RedirectToAction("Index");
        }
    }
}
