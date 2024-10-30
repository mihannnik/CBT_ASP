using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Options;
using CBT.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBT.Web.Controllers
{
    [Route("auth")]
    public class AuthController(IAuthService authService) : Controller
    {
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody]RegisterRequest request)
        {
            authService.Register(request);
            return Ok();
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (authService.GetAuthUser(request) is User user)
            {
                var token = authService.GetAuthToken(user);
                var refreshToken = authService.GetRefreshToken(user);
                HttpContext.Response.Cookies.Append(JWTOptions.CookiesName, token);
                HttpContext.Response.Cookies.Append(JWTOptions.RefreshCookiesName, refreshToken);
                return Ok("Succes");
            }
            return BadRequest("No such user");
        }
        [Route("logout")]
        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(JWTOptions.CookiesName);
            HttpContext.Response.Cookies.Delete(JWTOptions.RefreshCookiesName);
            return Ok();
        }
    }
}
