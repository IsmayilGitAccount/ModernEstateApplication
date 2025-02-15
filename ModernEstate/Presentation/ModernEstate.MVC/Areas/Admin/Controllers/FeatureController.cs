using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.FAQs;
using ModernEstate.MVC.Areas.Admin.ViewModels.Features;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController(AppDbContext _context) : Controller
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

            int count = await _context.Features.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var featureVMs = await _context.Features.Select(f => new GetAdminFeatureVM
            {
                Id = f.Id,
                FeatureName = f.FeatureName,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminFeatureVM> paginationVM = new PaginationVM<GetAdminFeatureVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = featureVMs
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
        public async Task<IActionResult> Create(CreateAdminFeatureVM featureVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                return View(featureVM);
            }

            bool result = await _context.Features.AnyAsync(f => f.FeatureName.Trim() == featureVM.FeatureName.Trim());

            if (result)
            {
                ModelState.AddModelError(nameof(CreateAdminFeatureVM.FeatureName), $"{featureVM.FeatureName} is already taken, please try again!");
                return View(featureVM);
            }

            Feature feature = new Feature()
            {
                FeatureName = featureVM.FeatureName,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _context.Features.AddAsync(feature);

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
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return NotFound();

            UpdateAdminFeatureVM featureVM = new UpdateAdminFeatureVM()
            {
                FeatureName = feature.FeatureName,
            };

            return View(featureVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminFeatureVM featureVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (id is null || id <= 0) return BadRequest();
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View(featureVM);
            }

            bool result = await _context.Features.AnyAsync(f => f.FeatureName.Trim() == featureVM.FeatureName.Trim() && f.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminFeatureVM.FeatureName), $"{featureVM.FeatureName} is already taken, please try again!");
                return View(featureVM);
            }

            feature.FeatureName = featureVM.FeatureName;

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
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return NotFound();

            _context.Features.Remove(feature);

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
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return NotFound();

            return View(feature);
        }

    }
}
