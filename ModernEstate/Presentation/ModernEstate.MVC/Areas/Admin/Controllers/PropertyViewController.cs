using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Areas.Admin.ViewModels.Types;
using ModernEstate.Areas.Admin.ViewModels.Views;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyViewController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new BadRequestException();

            int count = await _context.Views.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) throw new NotFoundException();

            var viewVMs = await _context.Views.Select(v => new GetAdminViewVM
            {
                Id = v.Id,
                ViewType = v.ViewType,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminViewVM> paginationVM = new PaginationVM<GetAdminViewVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = viewVMs
            };

            return View(paginationVM);
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
            if (id is null || id <= 0) throw new BadRequestException();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) throw new NotFoundException();

            UpdateAdminViewVM viewVM = new UpdateAdminViewVM()
            {
                ViewType = view.ViewType,
            };

            return View(viewVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminViewVM viewVM)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) throw new NotFoundException();

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
            if (id is null || id <= 0) throw new BadRequestException();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) throw new NotFoundException();

            _context.Views.Remove(view);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            View view = await _context.Views.FirstOrDefaultAsync(v => v.Id == id);
            if (view == null) throw new NotFoundException();

            return View(view);
        }

    }
}
