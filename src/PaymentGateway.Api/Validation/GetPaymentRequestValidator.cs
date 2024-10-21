using PaymentsGateway.Domain.Validators;

namespace PaymentsGateway.Api.Validation
{
    public static class GetPaymentRequestValidator
    {
        public static ValidationResult Validate(Guid id)
            => StandardValidators.ValidateUuid(nameof(id), id.ToString());
    }
}
