using PaymentGateway.Api.Models.Requests;
using PaymentsGateway.Domain.Validators;
using ValidationResult = PaymentsGateway.Domain.Validators.ValidationResult;

namespace PaymentsGateway.Api.Validation;

public class PostPaymentRequestValidator : IRequestValidator<PostPaymentRequest?>
{
    private readonly TimeProvider _timeProvider;

    public PostPaymentRequestValidator(TimeProvider timeProvider) 
    {
        _timeProvider = timeProvider;
    }

    public ValidationResult Validate(PostPaymentRequest? request)
    {
        if (request is null)
        {
            return StandardParameterErrors.RequestBodyRequired("$");
        }

        return ValidationResult.Combine(StandardValidators.ValidateRequired(nameof(request.Amount), request.Amount)
            .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.Currency), request.Currency))
            .ContinueIfSuccess(() => StandardValidators.ValidateCurrency(nameof(request.Currency), request.Currency))
            .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.CardNumber), request.CardNumber))
            .ContinueIfSuccess(() => StandardValidators.ValidateCardNumber(nameof(request.CardNumber), request.CardNumber))
            .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.ExpiryMonth), request.ExpiryMonth))
            .ContinueIfSuccess(() => StandardValidators.ValidateExpiryMonth(nameof(request.ExpiryMonth), request.ExpiryMonth))
            .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.ExpiryYear), request.ExpiryYear))
            .ContinueIfSuccess(() => StandardValidators.ValidateExpiryYear(nameof(request.ExpiryYear), request.ExpiryMonth, request.ExpiryYear, _timeProvider))
            .ContinueIfSuccess(() => StandardValidators.ValidateExpiryDate(nameof(request.ExpiryYear), request.ExpiryMonth, request.ExpiryYear, _timeProvider))
            .ContinueIfSuccess(() => StandardValidators.ValidateAmount(nameof(request.Amount), request.Amount))
            .ContinueIfSuccess(() => StandardValidators.ValidateRequired(nameof(request.Cvv), request.Cvv))
            .ContinueIfSuccess(() => StandardValidators.ValidateCvv(nameof(request.Cvv), request.Cvv)));
    }
}
