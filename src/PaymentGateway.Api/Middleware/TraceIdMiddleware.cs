using System.Diagnostics;
using PaymentGateway.Api.Constants;

namespace PaymentGateway.Api.Middleware;

public class TraceIdMiddleware
{
    private readonly RequestDelegate _next;

    public TraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Activity.Current?.TraceId;
        context.Response.Headers.Append(ApiHeaders.TraceId, traceId?.ToString());

        await _next(context);
    }
}