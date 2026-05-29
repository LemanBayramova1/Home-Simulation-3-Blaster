using Home_Simulation_3.Areas.Admin.ViewModels.Employee;
using Home_Simulation_3.DAL;
using Home_Simulation_3.Models;
using Home_Simulation_3.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home_Simulation_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees
                .Include(e => e.Position)
                .ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Position = await _context.Positions.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVm employeeVM)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();

            if (employeeVM.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View(employeeVM);
            }
            else
            {
                if (!employeeVM.ImageFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("ImageFile", "ImageFile must be an image");
                    return View(employeeVM);
                }
                if (employeeVM.ImageFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "ImageFile size must be 2MB");
                    return View(employeeVM);
                }
            }
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                FullAdress = employeeVM.FullAddress,
                PositionId = employeeVM.PositionId,
                ImageUrl = employeeVM.ImageFile.SaveImage(_env, "uploads/employees")
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            employee.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return NotFound();
            Employee employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();
            employee.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();

            if (id is null) return NotFound();

            Employee employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (employee == null) return NotFound();

            UpdateEmployeeVm employeeVM = new UpdateEmployeeVm
            {
                Name = employee.Name,
                FullAddress = employee.FullAdress,
                PositionId = employee.PositionId,
                ImageURL = employee.ImageUrl,
            };

            return View(employeeVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeVm employeeVM)
        {
            ViewBag.Position = await _context.Positions.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            Employee employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == employeeVM.Id);

            if (employee == null) return NotFound(); employee.Name = employeeVM.Name;
            employee.FullAdress = employeeVM.FullAddress;
            employee.PositionId = employeeVM.PositionId;
            if (employeeVM.ImageFile is not null)
            {
                employee.ImageUrl = employeeVM.ImageFile.SaveImage(_env, "uploads/employees");
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
