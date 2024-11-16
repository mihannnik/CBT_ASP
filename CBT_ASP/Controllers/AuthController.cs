using CBT.Application.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Requests;
using CBT.Infrastructure.Common.Options;
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
                var token = authService.CreateAuthToken(user);
                var refreshToken = authService.CreateRefreshToken(user);
                HttpContext.Response.Cookies.Append(JWTOptions.CookiesName, token);
                HttpContext.Response.Cookies.Append(JWTOptions.RefreshCookiesName, refreshToken);
                return Ok("Success");
            }
            return BadRequest("No such user");
        }
        [Route("logout")]
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(JWTOptions.CookiesName);
            HttpContext.Response.Cookies.Delete(JWTOptions.RefreshCookiesName);
            return Ok();
        }

        [Route("refresh")]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UseRefreshToken()
        {
            if (HttpContext.Request.Cookies.TryGetValue(JWTOptions.RefreshCookiesName, out string? token)
                && token is string)
            {
                HttpContext.Response.Cookies.Delete(JWTOptions.CookiesName);
                HttpContext.Response.Cookies.Delete(JWTOptions.RefreshCookiesName);
                if (authService.UseRefreshToken(token) is User authUser
                    && authService.CreateAuthToken(authUser) is string authToken
                    && authService.CreateRefreshToken(authUser) is string authRefreshToken)
                {
                    HttpContext.Response.Cookies.Append(JWTOptions.CookiesName, authToken);
                    HttpContext.Response.Cookies.Append(JWTOptions.RefreshCookiesName, authRefreshToken);
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}
