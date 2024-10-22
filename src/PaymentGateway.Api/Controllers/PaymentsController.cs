using System.Net;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Models.Requests;
using PaymentsGateway.Api.Constants;
using PaymentsGateway.Api.Extensions;
using PaymentsGateway.Api.Mappers;
using PaymentsGateway.Api.Util;
using PaymentsGateway.Api.Validation;
using PaymentsGateway.Domain.Handlers;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Validators;
using GetPaymentResponse = PaymentsGateway.Domain.Models.GetPaymentResponse;

namespace PaymentGateway.Api.Controllers;

[Route(ApiRoutes.Base)]
[ApiController]
public class PaymentsController : Controller
{
    private readonly IRequestHandler<CreatePaymentRequest, CreatePaymentResponse> _createPaymentRequestHandler;
    private readonly IRequestHandler<GetPaymentRequest, GetPaymentResponse> _getPaymentRequestHandler;
    private readonly IRequestValidator<PostPaymentRequest> _requestValidator;

    public PaymentsController(
        IRequestHandler<CreatePaymentRequest, CreatePaymentResponse> createPaymentRequestHandler,
        IRequestHandler<GetPaymentRequest, GetPaymentResponse> getPaymentRequestHandler,
        IRequestValidator<PostPaymentRequest> requestValidator)
    {
        _createPaymentRequestHandler = createPaymentRequestHandler;
        _getPaymentRequestHandler = getPaymentRequestHandler;
        _requestValidator = requestValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PostPaymentRequest paymentRequest)
    {
        var validationResult = _requestValidator.Validate(paymentRequest);
        if (validationResult.IsFailure)
        {
            var errors = validationResult.GetGroupErrors();
            return StatusCode((int)HttpStatusCode.BadRequest, ProblemDetailsHelper.InvalidParameters(errors, ApiRoutes.Base));
        }

        var createPaymentsResult = await _createPaymentRequestHandler.HandleAsync(paymentRequest.ToDomain());

        return createPaymentsResult.Match(
            success => StatusCode((int)HttpStatusCode.Created, success.PaymentDetails.ToPostPaymentResponse()),
            internalError => StatusCode(
                (int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
            );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var domainGetPaymentRequest = new GetPaymentRequest(id);

        var result = await _getPaymentRequestHandler.HandleAsync(domainGetPaymentRequest);

        return result.Match(
            success => Ok(success.PaymentDetails.ToGetPaymentResponse()),
            notFound => StatusCode(
                (int)HttpStatusCode.NotFound,
                ProblemDetailsHelper.PaymentNotFound(ApiRoutes.Base)),
            internalError => StatusCode(
                (int)HttpStatusCode.InternalServerError,
                ProblemDetailsHelper.InternalServerError(ApiRoutes.Base))
            );
    }
}