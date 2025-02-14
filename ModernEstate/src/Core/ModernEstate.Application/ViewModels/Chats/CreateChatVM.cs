using ModernEstate.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ModernEstate.Application.ViewModels.Chats
{
    public class CreateChatVM
    {
        public int AgentId { get; set; }  // Seçilən agentin ID-si
        public string Text { get; set; }  // Mesajın mətni
        public string UserId { get; set; } // İstifadəçinin ID-si
        public DateTime SentAt { get; set; } // Mesajın göndərildiyi vaxt

        public List<Agent> Agent { get; set; }  // Seçilən agent
        public List<Chat> Messages { get; set; }  // Əvvəlki mesajlar
    }

}
