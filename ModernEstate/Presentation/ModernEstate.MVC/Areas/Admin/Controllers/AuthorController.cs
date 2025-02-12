using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Extensions;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.MVC.Areas.Admin.ViewModels.Authors;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var authorVMs = await _context.Authors.Include(a=>a.Posts).Select(a=>new GetAdminAuthorVM
            {
                Id = a.Id,
                AuthorName = a.AuthorName,
                Photo = a.Photo,
                PostCount = a.Posts.Count,
            }).ToListAsync();

            return View(authorVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminAuthorVM authorVM)
        {
            if(!ModelState.IsValid)
            {
                return View(authorVM);
            }

            if (authorVM.Description.Length > 1000)
            {
                ModelState.AddModelError(nameof(authorVM.Description), "Description must be less than 1000 characters!");
                return View(authorVM);
            }

            Author author = new Author() 
            { 
                AuthorName = authorVM.AuthorName,
                Description = authorVM.Description
            };

            if (!authorVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(authorVM.Photo), "File type is incorrect");
                return View(authorVM);
            }
            if (!authorVM.Photo.ValidateSize(FileSize.MB, 2))
            {
                ModelState.AddModelError(nameof(authorVM.Photo), "File size is incorrect");
                return View(authorVM);
            }
            if (authorVM.Photo != null)
            {
               author.Photo = await authorVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "images");
            }

            await _context.Authors.AddAsync(author);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            UpdateAdminAuthorVM authorVM = new UpdateAdminAuthorVM()
            {
                AuthorName = author.AuthorName,
                Description = author.Description
            };

            return View(authorVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminAuthorVM authorVM)
        {
            if (id is null || id <= 0) return BadRequest();

            Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }

            if(authorVM.Photo != null)
            {
                if (!authorVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(authorVM.Photo), "File type is incorrect");
                    return View(authorVM);
                }
                if (!authorVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(authorVM.Photo), "File size is incorrect");
                    return View(authorVM);
                }

                author.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
                author.Photo = await authorVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "images");
            }

            authorVM.AuthorName = author.AuthorName;

            if (authorVM.Description.Length > 1000)
            {
                ModelState.AddModelError(nameof(authorVM.Description), "Description must be less than 1000 characters!");
                return View(authorVM);
            }
            authorVM.Description = author.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            if(author.Photo != null)
            {
                author.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
            }

            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return NotFound();

            return View(author);
        }
    }
}
