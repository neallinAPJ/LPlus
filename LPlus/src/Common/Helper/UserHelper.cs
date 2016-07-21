using Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class UserHelper
    {
        public static async Task SignInAsync(UserModel user, bool isPersistent, HttpContext context)
        {           
            await context.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String));
            claims.Add(new Claim("UserID", user.ID.ToString(), ClaimValueTypes.String));
            var userIdentity = new ClaimsIdentity("user");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            await context.Authentication.SignInAsync("MyCookieMiddlewareInstance", userPrincipal,
            new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                IsPersistent = false,
                AllowRefresh = false
            });
        }
        public static async Task SignOutAsync(HttpContext context)
        {
            await context.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
        }

    }
}
