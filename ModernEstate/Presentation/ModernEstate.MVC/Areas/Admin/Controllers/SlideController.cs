using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Exceptions;
using ModernEstate.Application.Utilities.Extensions;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.AdminSlides;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        string Root = Path.Combine("assets", "images");
        
        public async Task<IActionResult> Index(int page = 1)
        {

            if (page < 1) throw new BadRequestException();

            int count = await _context.Slides.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) throw new NotFoundException();

            var slides = await _context.Slides.Select(s=>new GetAdminSlideVM
            {
                Id = s.Id,
                Photo = s.Photo,
                Title = s.Title,
                Order = s.Order,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminSlideVM> paginationVM = new PaginationVM<GetAdminSlideVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = slides
            };

            return View(paginationVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminSlideVM slideVM)
        {
            if (!ModelState.IsValid)
            {
                return View(slideVM);
            }

            bool result = await _context.Slides.AnyAsync(s => s.Title.Trim() == slideVM.Title.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminSlideVM.Title), $"{slideVM.Title} is already taken!");
                return View(slideVM);
            }

            bool order = await _context.Slides.AnyAsync(s => s.Order == slideVM.Order);

            if (order)
            {
                ModelState.AddModelError(nameof(CreateAdminSlideVM.Order), $"{slideVM.Order} is already taken!");
                return View(slideVM);
            }

            if (slideVM.Order <= 0)
            {
                ModelState.AddModelError(nameof(CreateAdminSlideVM.Order), "Order must be greater than 0!");
                return View(slideVM);
            }

            Slide slide = new Slide()
            {
                CreatedAt = DateTime.Now,
                Description = slideVM.Description,
                IsDeleted = false,
                Title = slideVM.Title,
                Order = slideVM.Order,
                Price = slideVM.Price,
            };

            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateAdminSlideVM.Photo), "File type is incorrect, please try again!");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateAdminSlideVM.Photo), "File size is incorrect, please try again!");
                    return View(slideVM);
                }

                slide.Photo = await slideVM.Photo.CreatFileAsync(_env.WebRootPath, Root);
            }

            await _context.Slides.AddAsync(slide);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) throw new NotFoundException();

            UpdateAdminSlideVM slideVM = new UpdateAdminSlideVM()
            {
                Description = slide.Description,
                Order = slide.Order,
                Price = slide.Price,
                Title = slide.Title,
            };

            return View(slideVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminSlideVM slideVM)
        {
            if (id is null || id <= 0) throw new BadRequestException();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) throw new NotFoundException();

            if (!ModelState.IsValid)
            {
                return View(slideVM);
            }

            bool result = await _context.Slides.AnyAsync(s => s.Title.Trim() == slideVM.Title.Trim() && s.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminSlideVM.Title), $"{slideVM.Title} is already taken!");
                return View(slideVM);
            }

            bool order = await _context.Slides.AnyAsync(s => s.Order == slideVM.Order && s.Id != id);

            if (order)
            {
                ModelState.AddModelError(nameof(UpdateAdminSlideVM.Order), $"{slideVM.Order} is already taken!");
                return View(slideVM);
            }

            if (slideVM.Photo is not null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateAdminSlideVM.Photo), "File type is incorrect, please try again!");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(UpdateAdminSlideVM.Photo), "File size is incorrect, please try again!");
                    return View(slideVM);
                }
                slide.Photo.DeleteFile(_env.WebRootPath, Root);
                slide.Photo = await slideVM.Photo.CreatFileAsync(_env.WebRootPath, Root);
            }

            slide.Description = slideVM.Description;
            slide.Price = slideVM.Price;

            if (slideVM.Order <= 0)
            {
                ModelState.AddModelError(nameof(CreateAdminSlideVM.Order), "Order must be greater than 0!");
                return View(slideVM);
            }
            slide.Order = slideVM.Order;
            slide.Title = slideVM.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) throw new NotFoundException();

            if (slide.Photo is not  null)
            {
                slide.Photo.DeleteFile(_env.WebRootPath, Root);
            }

            _context.Slides.Remove(slide);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) throw new BadRequestException();

            Slide slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);

            if (slide == null) throw new NotFoundException();

            return View(slide);
        }
    }
}
