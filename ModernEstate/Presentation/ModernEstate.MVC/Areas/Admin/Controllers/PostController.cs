using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Extensions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.Posts;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.MVC.Areas.Admin.ViewModels.Parkings;
using ModernEstate.MVC.Areas.Admin.ViewModels.Posts;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Posts.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();


            var postVMs = await _context.Posts.Include(p => p.Agency).Include(p => p.Author).Select(p => new GetAdminPostVM
            {
                Id = p.Id,
                Title = p.Title,
                Photo = p.Photo,
                PostedBy = p.Author.AuthorName,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminPostVM> paginationVM = new PaginationVM<GetAdminPostVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = postVMs
            };

            return View(paginationVM);
        }

        public async Task<IActionResult> Create()
        {
            CreateAdminPostVM postVM = new CreateAdminPostVM()
            {
                Agency = await _context.Agencies.ToListAsync(),
                Author = await _context.Authors.ToListAsync(),
            };

            return View(postVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminPostVM postVM)
        {
            postVM.Agency = await _context.Agencies.ToListAsync();
            postVM.Author = await _context.Authors.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(postVM);
            }

            bool agency = await _context.Agencies.AnyAsync(a => a.Id == postVM.AgencyId);

            if (!agency)
            {
                ModelState.AddModelError(nameof(CreateAdminPostVM.AgencyId), "Agency is not exist");
                return View(postVM);
            }

            bool author = await _context.Authors.AnyAsync(a => a.Id == postVM.AuthorId);

            if (!author)
            {
                ModelState.AddModelError(nameof(CreateAdminPostVM.AuthorId), "Author is not exist");
                return View(postVM);
            }

            Post post = new Post()
            {
                AuthorId = postVM.AuthorId.Value,
                AgencyId = postVM.AgencyId.Value,
                PostedAt = DateTime.Now,
                Description = postVM.Description,
                Title = postVM.Title,
            };

            if (postVM.Photo is not null)
            {
                if (!postVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateAdminPostVM.Photo), "File type is incorrect, please try again!");
                    return View(postVM);
                }
                if (!postVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateAdminPostVM.Photo), "File size is incorrect, please try again!");
                    return View(postVM);
                }
                post.Photo = await postVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "images");
            }

            await _context.Posts.AddAsync(post);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post is null) return NotFound();

            UpdateAdminPostVM postVM = new UpdateAdminPostVM()
            {
                Agency = await _context.Agencies.ToListAsync(),
                Author = await _context.Authors.ToListAsync(),
                Title = post.Title,
                Description = post.Description,
                AgencyId = post.AgencyId,
                AuthorId = post.AuthorId,
                UpdatedAt = post.UpdatedAt,
                PostedAt = post.PostedAt,
            };

            return View(postVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminPostVM postVM)
        {
            postVM.Agency = await _context.Agencies.ToListAsync();
            postVM.Author = await _context.Authors.ToListAsync();

            if (id is null || id <= 0) return BadRequest();

            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(postVM);
            }

            bool agency = await _context.Agencies.AnyAsync(a => a.Id == postVM.AgencyId);

            if (!agency)
            {
                ModelState.AddModelError(nameof(UpdateAdminPostVM.AgencyId), "Agency is not exist");
                return View(postVM);
            }

            bool author = await _context.Authors.AnyAsync(a => a.Id == postVM.AuthorId);

            if (!author)
            {
                ModelState.AddModelError(nameof(UpdateAdminPostVM.AuthorId), "Author is not exist");
                return View(postVM);
            }

            if (postVM.Photo is not null)
            {
                if (!postVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateAdminPostVM.Photo), "File type is incorrect, please try again!");
                    return View(postVM);
                }
                if (!postVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(UpdateAdminPostVM.Photo), "File size is incorrect, please try again!");
                    return View(postVM);
                }
                post.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
                post.Photo = await postVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "images");
            }

            post.Title = postVM.Title;
            post.Description = postVM.Description;
            post.AgencyId = postVM.AgencyId.Value;
            post.AuthorId = postVM.AuthorId.Value;
            post.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post is null) return NotFound();

            if (post.Photo != null)
            {
                post.Photo.DeleteFile(_env.WebRootPath, "assets", "images");
            }

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Post post = await _context.Posts.Include(p => p.Agency).Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);

            if (post is null) return NotFound();

            return View(post);
        }
    }
}
