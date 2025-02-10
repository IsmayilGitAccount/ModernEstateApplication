using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Exteriors;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExteriorController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var exteriorVMs = await _context.Exteriors.Select(e => new GetAdminExteriorVM
            {
                Id = e.Id,
                ExteriorName = e.ExteriorType,
            }).ToListAsync();

            return View(exteriorVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminExteriorVM exteriorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(exteriorVM);
            }

            bool result = await _context.Exteriors.AnyAsync(e => e.ExteriorType.Trim() == exteriorVM.ExteriorName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminExteriorVM.ExteriorName), $"{exteriorVM.ExteriorName} is already taken, please try again!");
                return View(exteriorVM);
            }

            Exterior exterior = new Exterior()
            {
                ExteriorType = exteriorVM.ExteriorName,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Exteriors.AddAsync(exterior);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Exterior exterior = await _context.Exteriors.FirstOrDefaultAsync(e => e.Id == id);
            if (exterior == null) return BadRequest();

            UpdateAdminExteriorVM exteriorVM = new UpdateAdminExteriorVM()
            {
                ExteriorName = exterior.ExteriorType,
            };

            return View(exteriorVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminExteriorVM exteriorVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Exterior exterior = await _context.Exteriors.FirstOrDefaultAsync(e => e.Id == id);
            if (exterior == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(exteriorVM);
            }

            bool result = await _context.Exteriors.AnyAsync(e => e.ExteriorType.Trim() == exteriorVM.ExteriorName.Trim() && e.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminExteriorVM.ExteriorName), $"{exteriorVM.ExteriorName} is already taken, please try again!");
                return View(exteriorVM);
            }

            exterior.ExteriorType = exteriorVM.ExteriorName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Exterior exterior = await _context.Exteriors.FirstOrDefaultAsync(e => e.Id == id);
            if (exterior == null) return NotFound();

            _context.Exteriors.Remove(exterior);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Exterior exterior = await _context.Exteriors.FirstOrDefaultAsync(e => e.Id == id);
            if (exterior == null) return NotFound();

            return View(exterior);
        }

    }
}
