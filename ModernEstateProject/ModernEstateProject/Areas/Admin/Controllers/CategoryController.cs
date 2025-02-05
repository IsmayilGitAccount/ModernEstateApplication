using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ModernEstateProject.Areas.Admin.ViewModels.Categories;
using ModernEstateProject.Data;
using ModernEstateProject.Models;
using ModernEstateProject.Utilities.Extensions;
using ModernEstateProject.ViewModels.Properties;

namespace ModernEstateProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        string Root = Path.Combine("assets", "images");
        public async Task<IActionResult> Index()
        {
            var categoryVMs = await _context.Categories.Include(c=>c.Properties).Select(c => new GetAdminCategoryVM
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                Photo = c.CategoryPhoto,
                PropertyCount = c.Properties.Count,
            }).ToListAsync();

            return View(categoryVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminCategoryVM categoryVM)
        {
            if(!ModelState.IsValid)
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

            if(categoryVM.Photo != null)
            {
                if (!categoryVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(categoryVM.Photo), "File type is incorrect");
                    return View(categoryVM);
                }
                if (!categoryVM.Photo.ValidateSize(Utilities.Enums.FileSize.MB, 2))
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
            if (id is null || id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return BadRequest();

            UpdateAdminCategoryVM categoryVM = new UpdateAdminCategoryVM()
            {
                CategoryName = category.CategoryName,
            };

            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminCategoryVM categoryVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return BadRequest();
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
                if (!categoryVM.Photo.ValidateSize(Utilities.Enums.FileSize.MB, 2))
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
            if (id is null || id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return BadRequest();

            category.CategoryPhoto.DeleteFile(_env.WebRootPath, Root);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return BadRequest();

            return View(category);
        }
    }
}
