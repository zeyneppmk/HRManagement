using HRManagement.Data;
using HRManagement.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 1. Departman bazlı çalışan bilgileri
        public IActionResult EmployeesByDepartment()
        {
            var data = _context.Users
                .Include(u => u.Department)
                .GroupBy(u => u.Department.DepartmentName)
                .Select(g => new
                {
                    Department = g.Key,
                    EmployeeCount = g.Count(),
                    Employees = g.Select(x => x.FirstName + " " + x.LastName).ToList()
                }).ToList();

            return Json(data);
        }

        // 2. Departman müdürleri
        public IActionResult DepartmentManagers()
        {
            var managers = _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .Where(u => u.Role.RoleName == "Departman Müdürü")
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    Department = u.Department.DepartmentName
                }).ToList();

            return Json(managers);
        }

        // 3. Admin bilgileri
        public IActionResult Admins()
        {
            var admins = _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == "Admin")
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Email
                }).ToList();

            return Json(admins);
        }

        // 4. Departmana göre maaş istatistikleri
        public IActionResult DepartmentSalaryStats()
        {
            var stats = _context.UserDetails
                .Include(ud => ud.User)
                .ThenInclude(u => u.Department)
                .GroupBy(ud => ud.User.Department.DepartmentName)
                .Select(g => new
                {
                    Department = g.Key,
                    AvgSalary = g.Average(x => x.Salary),
                    MaxSalary = g.Max(x => x.Salary),
                    MinSalary = g.Min(x => x.Salary)
                }).ToList();

            return Json(stats);
        }

        // 5. A-Z, Z-A, Maaşa göre sıralı
        public IActionResult SortedUsers(string sortBy = "az")
        {
            var users = _context.Users.Include(x => x.UserDetail);

            var sorted = sortBy switch
            {
                "za" => users.OrderByDescending(x => x.FirstName),
                "salaryAsc" => users.OrderBy(x => x.UserDetail.Salary),
                "salaryDesc" => users.OrderByDescending(x => x.UserDetail.Salary),
                _ => users.OrderBy(x => x.FirstName)
            };

            return Json(sorted.Select(u => new
            {
                u.FirstName,
                u.LastName,
                Salary = u.UserDetail.Salary
            }).ToList());
        }

        public IActionResult NotificationsByType()
        {
            var grouped = _context.AppNotifications
                .GroupBy(n => n.Type)
                .Select(g => new
                {
                    Type = g.Key.ToString(),
                    Count = g.Count()
                }).ToList();

            return Json(grouped);
        }

        public IActionResult NotificationDetails()
        {
            var list = _context.AppNotifications
                .Include(n => n.Sender)
                .Include(n => n.Receiver)
                .Select(n => new
                {
                    Sender = n.Sender.FirstName + " " + n.Sender.LastName,
                    Receiver = n.Receiver.FirstName + " " + n.Receiver.LastName,
                    n.Title,
                    Type = n.Type.ToString(),
                    n.CreatedAt
                }).ToList();

            return Json(list);
        }


        // 7. Görevler kime verilmiş
        public IActionResult TaskAssignments()
        {
            var tasks = _context.TaskItems
                .Include(t => t.AssignedBy)
                .Include(t => t.AssignedTo)
                .Select(t => new
                {
                    t.Title,
                    AssignedBy = t.AssignedBy.FirstName + " " + t.AssignedBy.LastName,
                    AssignedTo = t.AssignedTo.FirstName + " " + t.AssignedTo.LastName,
                    Status = t.Status.ToString()
                }).ToList();

            return Json(tasks);
        }

        public IActionResult DepartmentSalaryReport()
        {
            var payments = _context.Payments
                .Include(p => p.User)
                    .ThenInclude(u => u.UserDetail)
                .Include(p => p.User)
                    .ThenInclude(u => u.Department)
                .Include(p => p.User)
                    .ThenInclude(u => u.Role)
                .ToList();

            var departmentTotals = payments
                .GroupBy(p => p.User.Department.DepartmentName)
                .Select(g => new
                {
                    Department = g.Key,
                    Total = g.Sum(x => x.Amount)
                }).ToList();

            var max = payments.OrderByDescending(p => p.Amount).FirstOrDefault();
            var min = payments.OrderBy(p => p.Amount).FirstOrDefault();

            return Json(new
            {
                Totals = departmentTotals,
                Max = new
                {
                    Amount = max.Amount,
                    User = max.User.FirstName + " " + max.User.LastName,
                    Role = max.User.Role.RoleName,
                    Department = max.User.Department.DepartmentName
                },
                Min = new
                {
                    Amount = min.Amount,
                    User = min.User.FirstName + " " + min.User.LastName,
                    Role = min.User.Role.RoleName,
                    Department = min.User.Department.DepartmentName
                }
            });
        }

        public IActionResult LeaveRequestNotifications()
        {
            var data = _context.AppNotifications
                .Where(n => n.Type == NotificationType.Leave && n.LeaveRequestId != null)
                .Include(n => n.Sender)
                .Include(n => n.Receiver)
                .Include(n => n.LeaveRequest)
                .Select(n => new
                {
                    Sender = n.Sender.FirstName + " " + n.Sender.LastName,
                    Receiver = n.Receiver.FirstName + " " + n.Receiver.LastName,
                    RequestDate = n.LeaveRequest!.StartDate,
                    Reason = n.LeaveRequest.Reason,
                    Status = n.LeaveRequest.Status.ToString(),
                    NotifiedAt = n.CreatedAt
                })
                .ToList();

            return Json(data);
        }

        public IActionResult TaskAssignmentNotifications()
        {
            var data = _context.AppNotifications
                .Where(n => n.Type == NotificationType.Task && n.TaskItemId != null)
                .Include(n => n.Receiver)
                    .ThenInclude(u => u.Role)
                .Include(n => n.TaskItem)
                .Include(n => n.Sender)
                .Select(n => new
                {
                    TaskTitle = n.TaskItem!.Title,
                    AssignedTo = n.Receiver.FirstName + " " + n.Receiver.LastName,
                    ReceiverRole = n.Receiver.Role.RoleName,
                    Sender = n.Sender.FirstName + " " + n.Sender.LastName,
                    Status = n.TaskItem.Status.ToString(),
                    NotifiedAt = n.CreatedAt
                })
                .ToList();

            return Json(data);
        }
        public IActionResult PaymentDetailsReport()
        {
            var data = _context.Payments
                .Include(p => p.User)
                    .ThenInclude(u => u.Department)
                .Include(p => p.User)
                    .ThenInclude(u => u.Pozisyons)
                .Select(p => new
                {
                    User = p.User.FirstName + " " + p.User.LastName,
                    Department = p.User.Department.DepartmentName,
                    Position = p.User.Pozisyons.FirstOrDefault() != null ? p.User.Pozisyons.FirstOrDefault().Title : "Pozisyon Yok",
                    Amount = p.Amount,
                    Period = p.PaymentPeriod,
                    Date = p.PaymentDate.ToShortDateString()
                })
                .ToList();

            return Json(data);
        }


        // 8. Genel sayılar ve özetler
        public IActionResult GeneralStats()
        {
            var totalUsers = _context.Users.Count();
            var totalDepartments = _context.Departments.Count();
            var avgRecordedSalary = _context.UserDetails.Average(x => x.Salary);
            var avgPaidSalary = _context.Payments.Average(p => p.Amount);
            var maxSalary = _context.UserDetails.Max(x => x.Salary);
            var totalTasks = _context.TaskItems.Count();

            return Json(new
            {
                totalUsers,
                totalDepartments,
                avgRecordedSalary,
                avgPaidSalary,
                maxSalary,
                totalTasks
            });
        }

        // 9. Event’leri türlerine göre ayır
        public IActionResult EventsGrouped()
        {
            var generalEvents = _context.Events
                .Where(e => e.Type == EventType.General)
                .ToList();

            var holidayEvents = _context.Events
                .Where(e => e.Type == EventType.Holiday)
                .ToList();

            return Json(new
            {
                generalEvents = generalEvents,
                officialHolidays = holidayEvents
            });
        }

    }
}
