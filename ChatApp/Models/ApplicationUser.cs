using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ChatHistory> ChatHistories { get; set; }
    }
}
