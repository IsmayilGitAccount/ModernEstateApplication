using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.Utilities.Extensions;
using ModernEstate.Application.ViewModels.AdminAgents;
using ModernEstate.Application.ViewModels.AdminPaginations;
using ModernEstate.Domain.Entities;
using ModernEstate.Domain.Enums;
using ModernEstate.MVC.Areas.Admin.ViewModels.Agents;
using ModernEstate.Persistence.Data;
namespace ModernEstate.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AgentController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        string Root = Path.Combine("assets", "images");

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) return BadRequest();

            int count = await _context.Agents.CountAsync();

            double total = Math.Ceiling((double)count / 3);

            if (page > total) return BadRequest();

            var agentVMs = await _context.Agents.Include(a => a.Agency).Select(a => new GetAdminAgentVM
            {
                Id = a.Id,
                AgencyName = a.Agency.AgencyName,
                Address = a.Address,
                Description = a.Description,
                Email = a.Email,
                FacebookLink = a.FacebookLink,
                FullName = a.FullName,
                InstagramLink = a.InstagramLink,
                LinkedinLink = a.LinkedinLink,
                XLink = a.XLink,
                PhoneNumber = a.PhoneNumber,
                Photo = a.Photo,
            }).Skip((page-1)*3).Take(3).ToListAsync();

            PaginationVM<GetAdminAgentVM> paginationVM = new PaginationVM<GetAdminAgentVM>()
            {
                TotalPage = total,
                CurrentPage = page,
                Items = agentVMs
            };

            return View(paginationVM);
        }

        public async Task<IActionResult> Create()
        {
            CreateAdminAgentVM agentVM = new CreateAdminAgentVM()
            {
                Agencies = await _context.Agencies.ToListAsync()
            };

            return View(agentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminAgentVM agentVM)
        {
            agentVM.Agencies = await _context.Agencies.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(agentVM);
            }

            bool result = await _context.Agencies.AnyAsync(a => a.Id == agentVM.AgencyId);

            if (!result)
            {
                ModelState.AddModelError(nameof(CreateAdminAgentVM), "Agencies is wrong!");
                return View(agentVM);
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(agentVM.Email, emailPattern))
            {
                ModelState.AddModelError(nameof(agentVM.Email), "Email format is incorrect!");
                return View(agentVM);
            }


            if (agentVM.Description.Length > 1000)
            {
                ModelState.AddModelError(nameof(agentVM.Description), "Description must be less than 1000 characters!");
                return View(agentVM);
            }

            Agent agent = new Agent()
            {
                Address = agentVM.Address,
                Description = agentVM.Description,
                AgencyId = agentVM.AgencyId,
                Email = agentVM.Email,
                FacebookLink = agentVM.FacebookLink,
                XLink = agentVM.XLink,
                InstagramLink = agentVM.InstagramLink,
                LinkedinLink = agentVM.LinkedinLink,
                FullName = agentVM.FullName,
                PhoneNumber = agentVM.PhoneNumber,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            if (agentVM.Photo is not null)
            {
                if (!agentVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File type is incorrect, please try again!");
                    return View(agentVM);
                }
                if (!agentVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File size is incorrect, please try again!");
                    return View(agentVM);
                }

                agent.Photo = await agentVM.Photo.CreatFileAsync(_env.WebRootPath, Root);
            }

            await _context.Agents.AddAsync(agent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            Agent agent = await _context.Agents.Include(a => a.Agency).FirstOrDefaultAsync(a => a.Id == id);
            if (agent == null) return NotFound();

            UpdateAdminAgentVM agentVM = new UpdateAdminAgentVM
            {
                Address = agent.Address,
                FacebookLink = agent.FacebookLink,
                XLink = agent.XLink,
                InstagramLink = agent.InstagramLink,
                LinkedinLink = agent.LinkedinLink,
                Description = agent.Description,
                PhoneNumber = agent.PhoneNumber,
                Email = agent.Email,
                FullName = agent.FullName,
                AgencyId = agent.AgencyId,
                Agencies = await _context.Agencies.ToListAsync()
            };

            return View(agentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminAgentVM agentVM)
        {
            agentVM.Agencies = await _context.Agencies.ToListAsync();

            if (id == null || id <= 0) return BadRequest();
            Agent agent = await _context.Agents.Include(a => a.Agency).FirstOrDefaultAsync(a => a.Id == id);
            if (agent == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(agentVM);
            }

            bool result = await _context.Agencies.AnyAsync(a => a.Id == agentVM.AgencyId);

            if (!result)
            {
                ModelState.AddModelError(nameof(UpdateAdminAgentVM), "Agencies is wrong!");
                return View(agentVM);
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(agentVM.Email, emailPattern))
            {
                ModelState.AddModelError(nameof(agentVM.Email), "Email format is incorrect!");
                return View(agentVM);
            }

            agent.Address = agentVM.Address;
            agent.Email = agentVM.Email;
            agent.FacebookLink = agentVM.FacebookLink;
            agent.PhoneNumber = agentVM.PhoneNumber;
            agent.LinkedinLink = agentVM.LinkedinLink;
            agent.XLink = agentVM.XLink;
            agent.InstagramLink = agentVM.InstagramLink;
            agent.FullName = agentVM.FullName;

            if (agentVM.Description.Length > 1000)
            {
                ModelState.AddModelError(nameof(agentVM.Description), "Description must be less than 1000 characters!");
                return View(agentVM);
            }
            agent.Description = agentVM.Description;
            agent.AgencyId = agentVM.AgencyId;

            if (agentVM.Photo is not null)
            {
                if (!agentVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateAdminAgentVM.Photo), "File type is incorrect, please try again!");
                    return View(agentVM);
                }
                if (!agentVM.Photo.ValidateSize(FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(UpdateAdminAgentVM.Photo), "File size is incorrect, please try again!");
                    return View(agentVM);
                }
                agent.Photo.DeleteFile(_env.WebRootPath, Root);
                agent.Photo = await agentVM.Photo.CreatFileAsync(_env.WebRootPath, Root);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            Agent agent = await _context.Agents.Include(a => a.Agency).FirstOrDefaultAsync(a => a.Id == id);
            if (agent == null) return NotFound();

            agent.Photo.DeleteFile(_env.WebRootPath, Root);
            _context.Remove(agent);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            Agent agent = await _context.Agents.Include(a => a.Agency).FirstOrDefaultAsync(a => a.Id == id);
            if (agent == null) return NotFound();

            return View(agent);
        }
    }
}

