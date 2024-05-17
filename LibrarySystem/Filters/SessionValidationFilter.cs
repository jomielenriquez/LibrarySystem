using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibrarySystem.Filters
{
    public class SessionValidationFilter
    {
        private readonly RequestDelegate _next;

        public SessionValidationFilter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var username = context.Session.GetString("UserAccountID");

            // If the username is not in the session and the path is not login, redirect to login
            if (username == null && !context.Request.Path.StartsWithSegments("/Account/Login"))
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context);
        }
    }
}
