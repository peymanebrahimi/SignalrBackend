using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class AuthDumpModel
    {
        private readonly AuthenticationService _authenticationService;

        public AuthDumpModel(IAuthenticationService authenticationService)
        {
            this._authenticationService = (AuthenticationService)authenticationService;
        }

        public IEnumerable<AuthenticationScheme> Schemes { get; set; }
        public AuthenticationScheme DefaultAuthenticate { get; set; }
        public AuthenticationScheme DefaultChallenge { get; set; }
        public AuthenticationScheme DefaultForbid { get; set; }
        public AuthenticationScheme DefaultSignIn { get; set; }
        public AuthenticationScheme DefaultSignOut { get; set; }

        public async Task OnGet()
        {
            Schemes = await _authenticationService.Schemes.GetAllSchemesAsync();
            DefaultAuthenticate = await _authenticationService.Schemes.GetDefaultAuthenticateSchemeAsync();
            DefaultChallenge = await _authenticationService.Schemes.GetDefaultChallengeSchemeAsync();
            DefaultForbid = await _authenticationService.Schemes.GetDefaultForbidSchemeAsync();
            DefaultSignIn = await _authenticationService.Schemes.GetDefaultSignInSchemeAsync();
            DefaultSignOut = await _authenticationService.Schemes.GetDefaultSignOutSchemeAsync();
        }
    }
}
