using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Areas.Admin.ViewModels.Types;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyTypeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) throw new BadRequestException();

            int count = await _context.Types.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) throw new NotFoundException();

            var typesVMs = await _context.Types.Select(t => new GetAdminTypesVM
            {
                Id = t.Id,
                TypesName = t.TypeName,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminTypesVM> paginationVM = new PaginationVM<GetAdminTypesVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = typesVMs
            };

            return View(paginationVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminTypesVM typeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(typeVM);
            }

            bool result = await _context.Types.AnyAsync(t => t.TypeName.Trim() == typeVM.TypesName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminTypesVM.TypesName), $"{typeVM.TypesName} is already taken, please try again!");
                return View(typeVM);
            }

            Types type = new Types()
            {
                TypeName = typeVM.TypesName,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Types.AddAsync(type);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) throw new NotFoundException();

            UpdateAdminTypesVM typeVM = new UpdateAdminTypesVM()
            {
                TypesName = type.TypeName,
            };
            return View(typeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminTypesVM typeVM)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) throw new NotFoundException();

            if (!ModelState.IsValid)
            {
                return View(typeVM);
            }

            bool result = await _context.Types.AnyAsync(t => t.TypeName.Trim() == typeVM.TypesName.Trim() && t.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminTypesVM.TypesName), $"{typeVM.TypesName} is already taken, please try again!");
                return View(typeVM);
            }

            type.TypeName = typeVM.TypesName;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) throw new NotFoundException();

            _context.Types.Remove(type);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) throw new NotFoundException();

            return View(type);
        }

    }
}
