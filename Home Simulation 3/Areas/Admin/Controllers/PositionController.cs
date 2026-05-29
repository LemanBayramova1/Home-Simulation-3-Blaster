using Home_Simulation_3.Areas.Admin.ViewModels.Position;
using Home_Simulation_3.DAL;
using Home_Simulation_3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home_Simulation_3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PositionController(AppDbContext db, IWebHostEnvironment env)
        {
            _context = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _context.Positions.ToListAsync();
            return View(positions);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVm positionVM)
        {
            if (!ModelState.IsValid) return View(positionVM);
            Position position = new Position
            {
                Name = positionVM.Name,
            };
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Position position = await _context.Positions.FindAsync(id);
            if (position == null) return NotFound();
            position.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null) return NotFound();
            Position position = await _context.Positions.FindAsync(id);
            if (position == null) return NotFound();
            position.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();
            Position position = await _context.Positions.FindAsync(id);
            if (position == null) return NotFound();
            UpdatePositionVm positionVM = new UpdatePositionVm
            {
                Name = position.Name,
            };
            return View(positionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVm positionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(positionVM);
            }
            Position position = await _context.Positions.FindAsync(positionVM.Id);
            if (position == null) return NotFound();
            position.Name = positionVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

