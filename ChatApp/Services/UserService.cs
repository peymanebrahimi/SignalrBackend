using System.Security.Claims;

namespace ChatApp.Services
{
    public interface IUserService
    {
        string GetUserId(ClaimsPrincipal user);
    }

    public class UserService : IUserService
    {
        public string GetUserId(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}
