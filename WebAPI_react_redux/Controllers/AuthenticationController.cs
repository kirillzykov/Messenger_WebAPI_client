using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebAPI_react_redux.Models;
using WebAPI_react_redux.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI_react_redux.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserServices dbUsers;

        public AuthenticationController(IUserServices context)
        {
            dbUsers = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<JsonResult> Login([Bind]LoginModel model)
        {
            User user = await dbUsers.FindUserEmailPassword(model.Email, model.Password);
            if (user.Email != null)
            {
                // await Authenticate(model.Email); // аутентификация
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                }, "Cookies");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await Request.HttpContext.SignInAsync("Cookies", claimsPrincipal);
                //return NoContent();
                return new JsonResult(user.UserId);
            }
            else
            {
                //return BadRequest();
                return new JsonResult("BadRequest");
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            User user = await dbUsers.FindUserEmail(model.Email);
            if (user.Email == null)
            {
                await dbUsers.InsertUser(new User { Name = model.Name, Email = model.Email, Password = model.Password });

                await Authenticate(model.Email);

                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return NoContent();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}