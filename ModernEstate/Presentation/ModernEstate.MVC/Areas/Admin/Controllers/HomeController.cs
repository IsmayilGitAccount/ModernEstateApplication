using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminAssignments;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController(AppDbContext _context) : Controller
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
            if (page < 1) return BadRequest();

            int count = await _context.Assignments.CountAsync();

            double total = Math.Ceiling((double)count / 5);

            if (page > total) return BadRequest();

            var assignments = await _context.Assignments.Select(a => new GetAdminAssignmentVM
            {
                Id = a.Id,
                Title = a.Title,
                Priority = a.Priority,
                Status = a.Status,
            }).Skip((page - 1) * 5).Take(5).ToListAsync();

            PaginationVM<GetAdminAssignmentVM> paginationVM = new PaginationVM<GetAdminAssignmentVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = assignments
            };

            return View(paginationVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
           
            if (id is null || id <= 0) return BadRequest();

            Assignment assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

            if (assignment is null) return NotFound();

            return View(assignment);
        }
    }
}
