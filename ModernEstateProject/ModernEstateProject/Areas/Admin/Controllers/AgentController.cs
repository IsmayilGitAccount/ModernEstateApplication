using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Areas.Admin.ViewModels.Agents;
using ModernEstateProject.Data;
using ModernEstateProject.Models;
using ModernEstateProject.Utilities.Extensions;
using ModernEstateProject.ViewModels.Agents;

namespace ModernEstateProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AgentController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        string Root = Path.Combine("assets", "images");
        public async Task<IActionResult> Index()
        {
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
            }).ToListAsync();

            return View(agentVMs);
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
                if (!agentVM.Photo.ValidateSize(Utilities.Enums.FileSize.MB, 2))
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
            if (agent == null) return BadRequest();

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
            if (agent == null) return BadRequest();

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

            agentVM.Address = agent.Address;
            agentVM.Email = agent.Email;
            agentVM.FacebookLink = agent.FacebookLink;
            agentVM.PhoneNumber = agent.PhoneNumber;
            agentVM.LinkedinLink = agent.LinkedinLink;
            agentVM.XLink = agent.XLink;
            agentVM.InstagramLink = agent.InstagramLink;
            agentVM.FullName = agent.FullName;
            agentVM.Description = agent.Description;
            agentVM.AgencyId = agent.AgencyId;

            if (agentVM.Photo is not null)
            {
                if (!agentVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File type is incorrect, please try again!");
                    return View(agentVM);
                }
                if (!agentVM.Photo.ValidateSize(Utilities.Enums.FileSize.MB, 2))
                {
                    ModelState.AddModelError(nameof(CreateAdminAgentVM.Photo), "File size is incorrect, please try again!");
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
            if (agent == null) return BadRequest();

            agent.Photo.DeleteFile(_env.WebRootPath, Root);

            _context.Remove(agent);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
