using System.Security.Claims;

namespace Pitter.Controllers;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private ClaimsPrincipal user;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        string method = context.Request.Method;
        if (method != "GET")
        {
            user = context.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = 401;
                return;
            }
            
            context.Items["userId"] = user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        await _next(context);
    }
}

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}