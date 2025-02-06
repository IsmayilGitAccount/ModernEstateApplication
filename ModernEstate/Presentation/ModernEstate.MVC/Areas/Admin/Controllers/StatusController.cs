using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Areas.Admin.ViewModels.Status;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatusController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var statusVMs = await _context.Status.Select(s => new GetAdminStatusVM
            {
                Id = s.Id,
                StatusName = s.StatusName,
            }).ToListAsync();

            return View(statusVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminStatusVM statusVM)
        {
            if (!ModelState.IsValid)
            {
                return View(statusVM);
            }

            bool result = await _context.Status.AnyAsync(s => s.StatusName.Trim() == statusVM.StatusName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminStatusVM.StatusName), $"{statusVM.StatusName} is already taken, please try again!");
                return View(statusVM);
            }

            Status status = new Status()
            {
                StatusName = statusVM.StatusName,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Status.AddAsync(status);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) return BadRequest();

            UpdateAdminStatusVM statusVM = new UpdateAdminStatusVM()
            {
                StatusName = status.StatusName,
            };

            return View(statusVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminStatusVM statusVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(statusVM);
            }

            bool result = await _context.Status.AnyAsync(s => s.StatusName.Trim() == statusVM.StatusName.Trim() && s.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminStatusVM.StatusName), $"{statusVM.StatusName} is already taken, please try again!");
                return View(statusVM);
            }

            status.StatusName = statusVM.StatusName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) return BadRequest();

            _context.Status.Remove(status);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Status status = await _context.Status.FirstOrDefaultAsync(s => s.Id == id);
            if (status == null) return BadRequest();

            return View(status);
        }

    }
}
