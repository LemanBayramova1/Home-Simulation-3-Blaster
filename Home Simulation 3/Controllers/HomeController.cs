using Home_Simulation_3.DAL;
using Home_Simulation_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home_Simulation_3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees
                .Include(e => e.Position)
                .ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Details(int id)
        {
            Employee employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(employee => employee.Id == id);
            return View(employee);
        }
    }
}
