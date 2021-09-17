using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class UserNewMessageViewModel
    {
        [Required]
        public string MessageContent { get; set; }
    }
}