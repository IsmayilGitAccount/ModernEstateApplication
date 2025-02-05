using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Areas.Admin.ViewModels.Roofs;
using ModernEstateProject.Data;
using ModernEstateProject.Models;

namespace ModernEstateProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoofController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var roofVMs = await _context.Roofs.Select(r => new GetAdminRoofVM
            {
                Id = r.Id,
                RoofType = r.RoofType,
            }).ToListAsync();

            return View(roofVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminRoofVM roofVM)
        {
            if (!ModelState.IsValid)
            {
                return View(roofVM);
            }

            bool result = await _context.Roofs.AnyAsync(r => r.RoofType.Trim() == roofVM.RoofType.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminRoofVM.RoofType), $"{roofVM.RoofType} is already taken, please try again!");
                return View(roofVM);
            }

            Roof roof = new Roof()
            {
                RoofType = roofVM.RoofType,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Roofs.AddAsync(roof);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return BadRequest();

            UpdateAdminRoofVM roofVM = new UpdateAdminRoofVM()
            {
                RoofType = roof.RoofType,
            };

            return View(roofVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminRoofVM roofVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(roofVM);
            }

            bool result = await _context.Roofs.AnyAsync(r => r.RoofType.Trim() == roofVM.RoofType.Trim() && r.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminRoofVM.RoofType), $"{roofVM.RoofType} is already taken, please try again!");
                return View(roofVM);
            }

            roof.RoofType = roofVM.RoofType;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return BadRequest();

            _context.Roofs.Remove(roof);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) return BadRequest();

            return View(roof);
        }
    }
}
