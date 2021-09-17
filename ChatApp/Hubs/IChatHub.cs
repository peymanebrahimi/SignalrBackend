using System.Threading.Tasks;
using ChatApp.Models;

namespace ChatApp.Hubs
{
    public interface IChatHub
    {
        Task UserIsTyping(UserIsTypingViewModel model);
        Task NewMessage(UserMessageViewModel model);
        Task UserIsOnline(UserJoinedChatViewModel model);
    }
}