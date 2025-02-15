using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Application.ViewModels.Chats;
using ModernEstate.Domain.Entities;
using ModernEstate.Persistence.Data;
using System.Security.Claims;

public class ChatController : Controller
{
    private readonly AppDbContext _context;

    public ChatController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Create(int agentId)
    {
        var agent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == agentId);

        if (agent == null)
        {
            return NotFound("Agent not found.");
        }

        var messages = await _context.Chats
            .Where(c => c.AgentId == agentId)
            .OrderBy(c => c.SentAt)
            .ToListAsync();

        var model = new CreateChatVM
        {
            AgentId = agentId,
            Agent = new List<Agent> { agent },
            Messages = messages
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Create(CreateChatVM chatVM)
    {
        chatVM.Agent = await _context.Agents.ToListAsync();
        if (chatVM.AgentId <= 0 || string.IsNullOrEmpty(chatVM.Text))
        {
            return BadRequest("Agent ID and Message text are required.");
        }

        var agent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == chatVM.AgentId);
        if (agent == null)
        {
            return NotFound("Agent not found.");
        }

        var chat = new Chat
        {
            Text = chatVM.Text,
            SentAt = DateTime.Now,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), 
            AgentId = chatVM.AgentId
        };

        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();

        var messages = await _context.Chats
            .Where(c => c.AgentId == chatVM.AgentId)
            .OrderBy(c => c.SentAt)
            .ToListAsync();

        chatVM.Messages = messages;

        return View(chatVM);
    }

}
