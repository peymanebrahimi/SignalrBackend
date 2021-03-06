using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }

        public ICollection<ChatHistory> ChatHistories { get; set; }
    }
}
