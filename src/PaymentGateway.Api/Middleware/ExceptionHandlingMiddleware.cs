using Hellang.Middleware.ProblemDetails;
using PaymentsGateway.Api.Constants;
using PaymentsGateway.Api.Extensions;
using System.Net;

namespace PaymentsGateway.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Conflict)
                {
                    throw new ProblemDetailsException(ExceptionDetailsMapping.IdempotencyKeyConflictError.CreateProblemDetails());
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.NotAcceptable)
                {
                    throw new ProblemDetailsException(ExceptionDetailsMapping.IdempotencyKeyReuseError.CreateProblemDetails());
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException && ex.Message.Contains(ApiHeaders.IdempotencyKey))
                {
                    throw new ProblemDetailsException(ExceptionDetailsMapping.IdempotencyKeyError.CreateProblemDetails());
                }
                _logger.LogError(ex, $"Unhandled exception: {ex.Message}");
                throw;
            }
        }
    }
}
