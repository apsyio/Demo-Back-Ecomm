using Apsy.Common.Api.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Aps.Apps.CueTheCurves.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAuthService authService;
        //private readonly FirebaseAppCreator firebaseAppCreator;

        public AccountController(IConfiguration configuration, IAuthService authService)
        {
            this.configuration = configuration;
            this.authService = authService;
            //this.firebaseAppCreator = firebaseAppCreator;
        }

        [HttpPost("emaillogin")]
        public async Task<ActionResult<AuthToken>> EmailLogin([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var apiKey = configuration["ApiKey"];
                var authConfig = new AuthConfig { ApiKey = apiKey };
                var authToken = await authService.EmailLogin(authConfig, email, password);
                return authToken;
            }
            catch
            {
                return NotFound("User Not Found");
            }
        }

        //[Route("token")]
        //[HttpPost]
        //public ActionResult<string> CustomToken([FromForm] string uid, [FromForm] string provider)
        //{
        //    if (string.IsNullOrEmpty(uid))
        //    {
        //        return BadRequest("uid is null");
        //    }

        //    try
        //    {
        //        FirebaseAuth auth = FirebaseAuth.GetAuth(firebaseAppCreator.GetFirebaseApp());
        //        auth.CreateUserAsync(new UserRecordArgs { DisplayName = "", Email = "", PhoneNumber = "", Uid = uid });
        //        var result = auth.CreateCustomTokenAsync($"{provider}:{uid}").Result;
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }

        //}
    }
}
