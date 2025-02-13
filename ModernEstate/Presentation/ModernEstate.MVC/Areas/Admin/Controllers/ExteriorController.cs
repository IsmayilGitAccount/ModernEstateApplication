using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Authors;
using ModernEstate.MVC.Areas.Admin.ViewModels.Exteriors;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExteriorController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Exteriors.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var exteriorVMs = await _context.Exteriors.Select(e => new GetAdminExteriorVM
            {
                Id = e.Id,
                ExteriorName = e.ExteriorType,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminExteriorVM> paginationVM = new PaginationVM<GetAdminExteriorVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = exteriorVMs
            };

            return View(paginationVM);
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
