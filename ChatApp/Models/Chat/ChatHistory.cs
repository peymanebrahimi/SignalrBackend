using System;

namespace ChatApp.Models.Chat
{
    public class ChatHistory
    {
        public ChatHistory()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MessageContent { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}