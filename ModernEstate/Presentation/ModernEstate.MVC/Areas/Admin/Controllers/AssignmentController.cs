using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.AdminAssignments;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Application.ViewModels.Agents;
using ModernEstate.Domain.Entities;
using ModernEstate.MVC.Areas.Admin.ViewModels.Agents;
using ModernEstate.Persistence.Data;

namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AssignmentController(AppDbContext _context) : Controller
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

            int count = await _context.Assignments.CountAsync();

            double total = Math.Ceiling((double)count / 5);

            if (page > total) return BadRequest();

            var assignments = await _context.Assignments.Select(a => new GetAdminAssignmentVM
            {
                Id = a.Id,
                Title = a.Title,
                Priority = a.Priority,
                Status = a.Status,
            }).Skip((page-1)*5).Take(5).ToListAsync();

            PaginationVM<GetAdminAssignmentVM> paginationVM = new PaginationVM<GetAdminAssignmentVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = assignments
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
        public async Task<IActionResult> Create(CreateAdminAssignmentVM assignmentVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(assignmentVM);
            }

            bool result = await _context.Assignments.AnyAsync(a => a.Title.Trim() == assignmentVM.Title.Trim());

            if(result)
            {
                ModelState.AddModelError(nameof(CreateAdminAssignmentVM.Title), $"{assignmentVM.Title} is already taken!");
                return View(assignmentVM);
            }

            Assignment assignment = new Assignment()
            {
                Title = assignmentVM.Title,
                Priority = assignmentVM.Priority,
                Status = assignmentVM.Status,
                Description = assignmentVM.Description,
            };

            await _context.Assignments.AddAsync(assignment);

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

            Assignment assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

            if (assignment is null) return NotFound();

            UpdateAdminAssignmentVM assignmentVM = new UpdateAdminAssignmentVM()
            {
                Title = assignment.Title,
                Priority = assignment.Priority,
                Status = assignment.Status,
                Description = assignment.Description,
            };

            return View(assignmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminAssignmentVM assignmentVM)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id is null || id <= 0) return BadRequest();

            Assignment assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

            if (assignment is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(assignmentVM);
            }

            bool result = await _context.Assignments.AnyAsync(a => a.Title.Trim() == assignmentVM.Title.Trim() && a.Id != id);

            if (result)
            {
                ModelState.AddModelError(nameof(UpdateAdminAssignmentVM.Title), $"{assignmentVM.Title} is already taken!");
                return View(assignmentVM);
            }
            assignment.Title = assignmentVM.Title;
            assignment.Priority = assignmentVM.Priority;
            assignment.Status = assignmentVM.Status;
            assignment.Description = assignmentVM.Description;

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

            Assignment assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

            if (assignment is null) return NotFound();

            return View(assignment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }

            if (id is null || id <= 0) return BadRequest();

            Assignment assignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);

            if (assignment is null) return NotFound();

            _context.Assignments.Remove(assignment);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
