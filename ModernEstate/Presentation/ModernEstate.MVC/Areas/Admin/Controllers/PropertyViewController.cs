using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Areas.Admin.ViewModels.Views;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyViewController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var viewVMs = await _context.Views.Select(v => new GetAdminViewVM
            {
                Id = v.Id,
                ViewType = v.ViewType,
            }).ToListAsync();

            return View(viewVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminViewVM viewVM)
        {
            if (!ModelState.IsValid)
            {
                return View(viewVM);
            }

            bool result = await _context.Views.AnyAsync(v => v.ViewType.Trim() == viewVM.ViewType.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminViewVM.ViewType), $"{viewVM.ViewType} is already taken, please try again!");
                return View(viewVM);
            }

            View view = new View()
            {
                ViewType = viewVM.ViewType,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Views.AddAsync(view);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            UpdateAdminViewVM viewVM = new UpdateAdminViewVM()
            {
                ViewType = view.ViewType,
            };

            return View(viewVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminViewVM viewVM)
        {
            if (id is null || id <= 0) return BadRequest();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(viewVM);
            }

            bool result = await _context.Views.AnyAsync(v => v.ViewType.Trim() == viewVM.ViewType.Trim() && v.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminViewVM.ViewType), $"{viewVM.ViewType} is already taken, please try again!");
                return View(viewVM);
            }

            view.ViewType = viewVM.ViewType;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            _context.Views.Remove(view);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) return NotFound();

            return View(view);
        }

    }
}
