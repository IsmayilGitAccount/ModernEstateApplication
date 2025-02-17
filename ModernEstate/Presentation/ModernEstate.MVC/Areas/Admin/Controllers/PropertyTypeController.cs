﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.Properties;
using ModernEstate.Areas.Admin.ViewModels.Types;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Property;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyTypeController(AppDbContext _context) : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (page < 1) return BadRequest();

            int count = await _context.Types.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

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
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminTypesVM typeVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
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
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) return NotFound();

            UpdateAdminTypesVM typeVM = new UpdateAdminTypesVM()
            {
                TypesName = type.TypeName,
            };
            return View(typeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminTypesVM typeVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) return NotFound();

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
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) return NotFound();

            _context.Types.Remove(type);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Types type = await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null) return NotFound();

            return View(type);
        }

    }
}
