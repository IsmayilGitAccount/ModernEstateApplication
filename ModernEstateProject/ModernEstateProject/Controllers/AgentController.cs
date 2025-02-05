using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstateProject.Data;
using ModernEstateProject.Models;
using ModernEstateProject.ViewModels.Agents;

namespace ModernEstateProject.Controllers
{
    public class AgentController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null || id <= 0) return BadRequest();

            Agent agent = await _context.Agents.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Id == id);

            if (agent == null) return BadRequest();

            AgentVM agentVM = new AgentVM()
            {
                Agent = agent,
                Properties = await _context.Properties.Include(p=>p.PropertyPhotos).Where(p => p.AgentId == agent.Id).ToListAsync()
            };

            return View(agentVM);
        }
    }
}
