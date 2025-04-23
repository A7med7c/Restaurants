
using Microsoft.AspNetCore.Connections.Features;
using System.Diagnostics;

namespace Restaurants.Api.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var timer =  Stopwatch.StartNew();
			await next.Invoke(context);
        timer.Stop();
        
        if(timer.ElapsedMilliseconds / 1000 > 4)
        {
            logger.LogInformation("Request {[Verb]} at {Path} took {Time} ms",
                context.Request.Method, context.Request.Path, timer.ElapsedMilliseconds);
        }
    }
}
