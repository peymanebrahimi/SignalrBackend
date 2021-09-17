using ChatApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Hosting;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Controllers
{
    [Authorize(AuthenticationSchemes = "IdentityServerJwt")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _loger;
        private readonly IAuthenticationService _authenticationService;

        public TestController(ILogger<TestController> loger,
            IAuthenticationService authenticationService)
        {
            _loger = loger;
            this._authenticationService = authenticationService;
        }

        [HttpGet("GetAuth")]
        public async Task<ActionResult> GetAuth()
        {
            _loger.LogInformation("inside GetAuth");

            var model = new AuthDumpModel(_authenticationService);

            await model.OnGet();
            return Ok(model);
        }

        [HttpGet("GetUser")]
        public ActionResult GetUser()
        {
            _loger.LogInformation("inside GetUser");

            var authTypes = User.Identities.Select(x => x.AuthenticationType).ToList();

            var userClaims = User.Claims.Select(x => new
            {
                x.Issuer,
                x.OriginalIssuer,
                x.Type,
                x.Value,
                x.ValueType
            }).ToList();

            var userIdentity = new
            {
                User.Identity.Name,
                User.Identity.AuthenticationType,
                User.Identity.IsAuthenticated
            };

            return Ok(new { authTypes, userClaims, userIdentity });
        }
    }
}