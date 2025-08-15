using AutoMapper;
using HRManagement.Data.Repository.IRepository;
using HRManagement.DTOs;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaskItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TaskItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var taskItems = _unitOfWork.TaskItem.GetAll(includeProperties: "AssignedTo,AssignedTo.Department");

            // DTO listesi
            var taskDtos = taskItems.Select(task => new TaskItemDto
            {
                TaskItemId = task.TaskItemId,
                Title = task.Title,
                Description = task.Description,
                AssignedDate = task.AssignedDate,
                DueDate = task.DueDate,
                AssignedById = task.AssignedById,
                AssignedToId = task.AssignedToId,
                Status = (int)task.Status,

                // 👇 Ek bilgiler
                AssignedToName = task.AssignedTo.FirstName + " " + task.AssignedTo.LastName,
                DepartmentName = task.AssignedTo.Department?.DepartmentName ?? "-"
            }).ToList();

            ViewBag.PlannedCount = taskItems.Count(x => x.Status == HRManagement.Entitites.TaskStatus.Planned);
            ViewBag.InProgressCount = taskItems.Count(x => x.Status == HRManagement.Entitites.TaskStatus.InProgress);
            ViewBag.CompletedCount = taskItems.Count(x => x.Status == HRManagement.Entitites.TaskStatus.Completed);
            ViewBag.IncompletedCount = taskItems.Count(x => x.Status != HRManagement.Entitites.TaskStatus.Completed);

            return View(taskDtos);
        }

    }
}
