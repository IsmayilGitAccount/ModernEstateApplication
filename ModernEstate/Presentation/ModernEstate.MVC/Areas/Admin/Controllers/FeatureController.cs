using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Features;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var featureVMs = await _context.Features.Select(f => new GetAdminFeatureVM
            {
                Id = f.Id,
                FeatureName = f.FeatureName,
            }).ToListAsync();

            return View(featureVMs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminFeatureVM featureVM)
        {
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
            if (id is null || id <= 0) return BadRequest();
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return BadRequest();

            UpdateAdminFeatureVM featureVM = new UpdateAdminFeatureVM()
            {
                FeatureName = feature.FeatureName,
            };

            return View(featureVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminFeatureVM featureVM)
        {
            if (id is null || id <= 0) return BadRequest();
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return BadRequest();
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
            if (id is null || id <= 0) return BadRequest();
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return BadRequest();

            _context.Features.Remove(feature);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Feature feature = await _context.Features.FirstOrDefaultAsync(f => f.Id == id);
            if (feature == null) return BadRequest();

            return View(feature);
        }

    }
}
