﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Constants;

namespace PaymentGateway.Api.Extensions;

internal record ExceptionDetails(HttpStatusCode StatusCode, string Type, string Title, string Detail);

internal static class ExceptionDetailsMapping
{
    internal static ExceptionDetails ForException(Exception e) => ServiceError;

    internal static ProblemDetails CreateProblemDetails(this ExceptionDetails exceptionDetails)
        => new()
        {
            Type = exceptionDetails.Type,
            Status = (int)exceptionDetails.StatusCode,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail
        };

    private static readonly ExceptionDetails ServiceError = new(HttpStatusCode.InternalServerError,
        ErrorMessages.InternalServerErrorType,
        ErrorMessages.InternalServerError,
        ErrorMessages.InternalServerErrorDetail);
}