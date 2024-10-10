
using Microsoft.AspNetCore.Mvc;

namespace MVCwithWebApi.Web.ExceptionHandling
{
    public class Middleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)StatusCodes.Status500InternalServerError,
                    Instance = context.Request.Path
                    
                };
                context.Response.Redirect("/");
            }
        }
    }
}
