using HRManagement.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using HRManagement.Entitites;
using TaskStatus = HRManagement.Entitites.TaskStatus;

namespace HRManagement.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var today = DateTime.Today;

            // 1. Sayı Kartları
            var totalUsers = _unitOfWork.User.GetAll().Count();
            var totalDepartments = _unitOfWork.Department.GetAll().Count();
            var totalPositions = _unitOfWork.Pozisyon.GetAll().Count();

            var pendingSalaryCount = _unitOfWork.TaskItem.GetAll(x => x.Status == TaskStatus.Planned).Count();

            var todayHolidays = _unitOfWork.LeaveRequest
                .GetAll(x => x.StartDate <= today && x.EndDate >= today).Count();

            var thisMonthEventCount = _unitOfWork.Event.GetAll(x =>
              x.StartDate.Month == today.Month &&
              x.StartDate.Year == today.Year
          ).Count();


            // 2. Grafik Verileri
            var employees = _unitOfWork.UserDetail.GetAll(includeProperties: "User");

            var departmentSalaryStats = employees
                .GroupBy(e => e.User?.DepartmentId)
                .Select(g => new
                {
                    DepartmentId = g.Key ?? 0,
                    TotalSalary = g.Sum(x => x.Salary ?? 0)
                }).ToList();

            var genderStats = employees
                .GroupBy(e => e.Gender)
                .Select(g => new
                {
                    Gender = g.Key,
                    Count = g.Count()
                }).ToList();

            var monthlySalaries = employees
                .Where(e => e.StartDate.HasValue)
                .GroupBy(e => e.StartDate.Value.Month)
                .Select(g => new
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Total = g.Sum(x => x.Salary ?? 0)
                }).ToList();

            // 🔧 Razor'da Select hatası olmaması için controller'da listeye dönüştür
            ViewBag.GenderLabels = genderStats.Select(g => g.Gender).ToList();
            ViewBag.GenderCounts = genderStats.Select(g => g.Count).ToList();

            ViewBag.DepartmentLabels = departmentSalaryStats.Select(d => $"Departman {d.DepartmentId}").ToList();
            ViewBag.DepartmentTotals = departmentSalaryStats.Select(d => d.TotalSalary).ToList();

            ViewBag.MonthLabels = monthlySalaries.Select(m => m.Month).ToList();
            ViewBag.MonthTotals = monthlySalaries.Select(m => m.Total).ToList();

            // 3. Devam Durumu
            var todayLeaves = _unitOfWork.LeaveRequest
                .GetAll(x => x.StartDate <= today && x.EndDate >= today)
                .Select(x => x.UserId)
                .ToList();

            var allUserIds = _unitOfWork.User.GetAll().Select(x => x.UserId).ToList();
            var activeUserCount = allUserIds.Count - todayLeaves.Count;

            // 4. Top Performers
            var topPerformers = _unitOfWork.TaskItem.GetAll(x => x.Status == TaskStatus.Completed)
                .GroupBy(x => x.AssignedToId)
                .Select(g => new
                {
                    UserId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            var users = _unitOfWork.User.GetAll(includeProperties: "UserDetail,Department").ToList();

            var userList = users.Select(u => new
            {
                FullName = u.FirstName + " " + u.LastName,
                DepartmentName = u.Department?.DepartmentName ?? "Yok",
                StartDate = u.UserDetail?.StartDate.HasValue == true? u.UserDetail.StartDate.Value.ToShortDateString():"Bilinmiyor",


                Salary = u.UserDetail?.Salary ?? 0
            }).ToList();

            ViewBag.UserDetails = userList;

            // ViewBag Aktarımı
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalDepartments = totalDepartments;
            ViewBag.TotalPositions = totalPositions;
            ViewBag.PendingSalary = pendingSalaryCount;
            ViewBag.TodayHoliday = todayHolidays;
            ViewBag.ThisMonthEvent = thisMonthEventCount;
            ViewBag.ActiveUsers = activeUserCount;
            ViewBag.LeaveUsers = todayLeaves.Count;
            ViewBag.TopPerformers = topPerformers;

            return View();
        }
    }
}
