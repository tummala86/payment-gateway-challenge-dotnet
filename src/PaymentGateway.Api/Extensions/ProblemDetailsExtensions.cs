using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PaymentGateway.Api.Constants;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

namespace PaymentGateway.Api.Extensions;

internal static class ProblemDetailsExtensions
{
    internal static void ConfigureProblemDetails(ProblemDetailsOptions options)
    {
        options.IncludeExceptionDetails = (ctx, ex) => false;

        options.TraceIdPropertyName = ApiHeaders.TraceId;
        options.GetTraceId = GetTraceId;
        options.OnBeforeWriteDetails = ApplyProblemDetailsDefaults;

        options.MapFromExceptionDetails<Exception>(ExceptionDetailsMapping.ForException);

        options.AllowedHeaderNames.Add(ApiHeaders.TraceId);
    }

    private static void MapFromExceptionDetails<TException>(this ProblemDetailsOptions options,
        Func<TException, ExceptionDetails> getExceptionDetails) where TException : Exception
    {
        options.Map<TException>(ex =>
        {
            var exceptionDetails = getExceptionDetails(ex);
            var problemDetails = exceptionDetails.CreateProblemDetails();
            return problemDetails;
        });
    }

    internal static string? GetTraceId(HttpContext context)
    {
        if (context.Response.Headers.TryGetValue(ApiHeaders.TraceId, out var traceIdValues))
        {
            var traceId = traceIdValues.FirstOrDefault();
            if (traceId is not null)
                return traceId;
        }
        return Guid.NewGuid().ToString() ?? context.TraceIdentifier;
    }

    internal static void ApplyProblemDetailsDefaults(HttpContext context, ProblemDetails problemDetails)
    {
        var status = problemDetails.Status ?? context.Response.StatusCode;

        problemDetails.Status = status;
        problemDetails.Title ??= ReasonPhrases.GetReasonPhrase(status);
        problemDetails.Type ??= problemDetails.Type;
        problemDetails.Instance ??= context.Request.Path;

        if (!problemDetails.Extensions.ContainsKey(ApiHeaders.TraceId))
        {
            problemDetails.Extensions[ApiHeaders.TraceId] = GetTraceId(context);
        }

    }
}