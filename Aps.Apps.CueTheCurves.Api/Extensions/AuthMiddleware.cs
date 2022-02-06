using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aps.Apps.CueTheCurves.Api.Extensions
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUserRepository userRepo)
        {
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                var externalId = httpContext.User.FindFirstValue("user_id");

                var user = userRepo.GetByExternalId(externalId);

                var claims = new List<Claim>
                    {
                        new Claim("user", JsonConvert.SerializeObject(user))
                    };

                var appIdentity = new ClaimsIdentity(claims);
                httpContext.User.AddIdentity(appIdentity);
            }

            await _next(httpContext);
        }
    }
}
