﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Extensions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.Agencies;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.MVC.Areas.Admin.ViewModels.Agencies;
using ModernEstate.MVC.Areas.Admin.ViewModels.Categories;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        string Root = Path.Combine("assets", "images");
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

            int count = await _context.Categories.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var categoryVMs = await _context.Categories.Include(c => c.Properties).Select(c => new GetAdminCategoryVM
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Photo = c.CategoryPhoto,
                PropertyCount = c.Properties.Count,
            }).Skip((page - 1) * 3).Take(3).ToListAsync();

            PaginationVM<GetAdminCategoryVM> paginationVM = new PaginationVM<GetAdminCategoryVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = categoryVMs
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
        public async Task<IActionResult> Create(CreateAdminCategoryVM categoryVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            bool result = await _context.Categories.AnyAsync(e => e.CategoryName.Trim() == categoryVM.CategoryName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminCategoryVM.CategoryName), $"{categoryVM.CategoryName} is already taken, please try again!");
                return View(categoryVM);
            }

            Category category = new Category()
            {
                CategoryName = categoryVM.CategoryName,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            if (categoryVM.Photo != null)
            {
                if (!categoryVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(categoryVM.Photo), "File type is incorrect");
                    return View(categoryVM);
                }
                if (!categoryVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(categoryVM.Photo), "File size is incorrect");
                    return View(categoryVM);
                }

                category.CategoryPhoto = await categoryVM.Photo.CreatFileAsync(_env.WebRootPath, Root);
            }

            await _context.Categories.AddAsync(category);

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
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();

            UpdateAdminCategoryVM categoryVM = new UpdateAdminCategoryVM()
            {
                CategoryName = category.CategoryName,
            };

            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminCategoryVM categoryVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id is null || id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            bool result = await _context.Categories.AnyAsync(e => e.CategoryName.Trim() == categoryVM.CategoryName.Trim() && e.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminCategoryVM.CategoryName), $"{categoryVM.CategoryName} is already taken, please try again!");
                return View(categoryVM);
            }

            if (categoryVM.Photo != null)
            {
                if (!categoryVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(categoryVM.Photo), "File type is incorrect");
                    return View(categoryVM);
                }
                if (!categoryVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(categoryVM.Photo), "File size is incorrect");
                    return View(categoryVM);
                }
                category.CategoryPhoto.DeleteFile(_env.WebRootPath, Root);
                category.CategoryPhoto = await categoryVM.Photo.CreatFileAsync(_env.WebRootPath, Root);
            }

            category.CategoryName = categoryVM.CategoryName;

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
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();

            if (category.CategoryPhoto is not null)
            {
                category.CategoryPhoto.DeleteFile(_env.WebRootPath, Root);
            }

            _context.Categories.Remove(category);

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
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }
    }
}
