using PaymentsGateway.Domain.Validators;

namespace PaymentsGateway.Api.Validation
{
    public interface IRequestValidator<in T>
    {
        ValidationResult Validate(T request);
    }
}
