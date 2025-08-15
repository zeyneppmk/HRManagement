using HRManagement.Data.Repository.IRepository;
using HRManagement.Entitites;
using HRManagement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppNotificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppNotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // 🔹 GET: /api/AppNotification
        [HttpGet]
        public IActionResult GetAll()
        {
            var notifications = _unitOfWork.AppNotification
                .GetAll(includeProperties: "Sender,Receiver");

            var result = notifications.Select(n => new AppNotificationDto
            {
                NotificationId = n.NotificationId,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type.ToString(),
                Status = n.Status?.ToString(),
                CreatedAt = n.CreatedAt,
                SenderId = n.SenderId,
                ReceiverId = n.ReceiverId,
                TaskItemId = n.TaskItemId,
                LeaveRequestId = n.LeaveRequestId,
                SenderName = n.Sender.FirstName + " " + n.Sender.LastName,
                ReceiverName = n.Receiver.FirstName + " " + n.Receiver.LastName
            });

            return Ok(result);
        }

        // 🔹 GET: /api/AppNotification/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var notification = _unitOfWork.AppNotification.Get(id);
            if (notification == null)
                return NotFound();

            return Ok(notification);
        }

        // 🔹 POST: /api/AppNotification
        [HttpPost]
        public IActionResult Create([FromBody] AppNotificationDto dto)
        {
            if (string.IsNullOrEmpty(dto.Type))
                return BadRequest("Type is required.");

            var entity = new AppNotification
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Title = dto.Title ?? "",
                Message = dto.Message ?? "",
                Type = Enum.Parse<NotificationType>(dto.Type, true),
                Status = string.IsNullOrEmpty(dto.Status) ? null : Enum.Parse<NotificationStatus>(dto.Status, true),
                TaskItemId = dto.TaskItemId,
                LeaveRequestId = dto.LeaveRequestId,
                CreatedAt = dto.CreatedAt
            };

            _unitOfWork.AppNotification.Add(entity);
            _unitOfWork.Save();

            return Ok(entity);
        }

        // 🔹 PUT: /api/AppNotification/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AppNotificationDto dto)
        {
            var existing = _unitOfWork.AppNotification.Get(id);
            if (existing == null)
                return NotFound();

            existing.Title = dto.Title ?? existing.Title;
            existing.Message = dto.Message ?? existing.Message;
            existing.Type = Enum.Parse<NotificationType>(dto.Type ?? existing.Type.ToString(), true);
            existing.Status = string.IsNullOrEmpty(dto.Status) ? null : Enum.Parse<NotificationStatus>(dto.Status, true);
            existing.TaskItemId = dto.TaskItemId;
            existing.LeaveRequestId = dto.LeaveRequestId;

            _unitOfWork.AppNotification.Update(existing);
            _unitOfWork.Save();

            return NoContent();
        }

        // 🔹 DELETE: /api/AppNotification/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var notification = _unitOfWork.AppNotification.Get(id);
            if (notification == null)
                return NotFound();

            _unitOfWork.AppNotification.Remove(notification);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
