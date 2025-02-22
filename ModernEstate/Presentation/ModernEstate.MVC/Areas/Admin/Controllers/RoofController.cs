using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.AdminRoofs;
using ModernEstate.Areas.Admin.ViewModels.Views;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoofController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new BadRequestException();

            int count = await _context.Roofs.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) throw new NotFoundException();

            var roofVMs = await _context.Roofs.Select(r => new GetAdminRoofVM
            {
                Id = r.Id,
                RoofType = r.RoofType,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminRoofVM> paginationVM = new PaginationVM<GetAdminRoofVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = roofVMs
            };

            return View(paginationVM);
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
            if (id is null || id <= 0) throw new BadRequestException();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) throw new NotFoundException();

            UpdateAdminRoofVM roofVM = new UpdateAdminRoofVM()
            {
                RoofType = roof.RoofType,
            };

            return View(roofVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminRoofVM roofVM)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) throw new NotFoundException();

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
            if (id is null || id <= 0) throw new BadRequestException();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) throw new NotFoundException();

            _context.Roofs.Remove(roof);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Roof roof = await _context.Roofs.FirstOrDefaultAsync(r => r.Id == id);
            if (roof == null) throw new NotFoundException();

            return View(roof);
        }
    }
}
