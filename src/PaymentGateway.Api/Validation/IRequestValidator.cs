using PaymentGateway.Domain.Validators;

namespace PaymentGateway.Api.Validation;

public interface IRequestValidator<in T>
{
    ValidationResult Validate(T request);
}