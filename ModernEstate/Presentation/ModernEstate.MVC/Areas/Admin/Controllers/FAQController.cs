using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Categories;
using ModernEstate.MVC.Areas.Admin.ViewModels.FAQs;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.FAQs.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var faqVMs = await _context.FAQs.Include(f=>f.Agency).Select(f=>new GetAdminFAQVM
            {
                Id = f.Id,
                Question = f.Question,
                AgencyName = f.Agency.AgencyName,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminFAQVM> paginationVM = new PaginationVM<GetAdminFAQVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = faqVMs
            };

            return View(paginationVM);
        }

        public async Task<IActionResult> Create()
        {
            CreateAdminFAQVM faqVM = new CreateAdminFAQVM()
            {
                Agency = await _context.Agencies.ToListAsync(),
            };

            return View(faqVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminFAQVM faqVM)
        {
            faqVM.Agency = await _context.Agencies.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(faqVM);
            }

            var result = await _context.Agencies.AnyAsync(a => a.Id == faqVM.AgencyId);

            if (!result)
            {
                ModelState.AddModelError(nameof(CreateAdminFAQVM.AgencyId), "Agency is not exists");
                return View(faqVM);
            }

            FAQ faq = new FAQ()
            {
                Question = faqVM.Question,
                Answer = faqVM.Answer,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                AgencyId = faqVM.AgencyId.Value,
            };

            await _context.FAQs.AddAsync(faq);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            FAQ faq = await _context.FAQs.FirstOrDefaultAsync(f=>f.Id == id);

            if (faq == null) return NotFound();

            UpdateAdminFAQVM faqVM = new UpdateAdminFAQVM()
            {
                Agency = await _context.Agencies.ToListAsync(),
                Question = faq.Question,
                Answer = faq.Answer,
                AgencyId = faq.AgencyId
            };

            return View(faqVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminFAQVM faqVM)
        {
            faqVM.Agency = await _context.Agencies.ToListAsync();

            if (id is null || id <= 0) return BadRequest();

            FAQ faq = await _context.FAQs.FirstOrDefaultAsync(f => f.Id == id);

            if (faq == null) return NotFound();

            var result = await _context.Agencies.AnyAsync(a => a.Id == faqVM.AgencyId);

            if (!result)
            {
                ModelState.AddModelError(nameof(CreateAdminFAQVM.AgencyId), "Agency is not exists");
                return View(faqVM);
            }

            faq.Question = faqVM.Question;
            faq.Answer = faqVM.Answer;
            faq.AgencyId = faqVM.AgencyId.Value;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            FAQ faq = await _context.FAQs.FirstOrDefaultAsync(f => f.Id == id);

            if (faq == null) return NotFound();

            _context.FAQs.Remove(faq);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            FAQ faq = await _context.FAQs.Include(f=>f.Agency).FirstOrDefaultAsync(f => f.Id == id);

            if (faq == null) return NotFound();

            return View(faq);
        }
    }
}
