using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatApp.Models.Chat;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Hubs
{
    [Authorize(AuthenticationSchemes = "IdentityServerJwt")]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.Others.SendAsync("ReceiveMessage", user, message);
        //}

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.UserIsOnline(new UserJoinedChatViewModel { UserName = Context?.User?.Identity?.Name });

            await base.OnConnectedAsync();
        }

        public async Task OnNewMessage(UserNewMessageViewModel model)
        {

            var chat = new ChatHistory
            {
                MessageContent = model.MessageContent,
                UserId = (Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            };

            _db.ChatHistories.Add(chat);

            await _db.SaveChangesAsync();

            var message = new UserMessageViewModel
            {
                MessageContent = chat.MessageContent,
                MessageDate = chat.Date.ToString("g"),
                UserName = Context?.User?.Identity?.Name,
                UserId = (Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            };
            await Clients.All.NewMessage(message);
        }

        public async Task OnUserTyping()
        {
            var userName = Context?.User?.Identity?.Name;

            await Clients.Others.UserIsTyping(new UserIsTypingViewModel { UserName = userName });
        }
    }
}
