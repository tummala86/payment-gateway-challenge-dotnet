namespace PaymentsGateway.Api.Validation
{
    public interface IRequestValidator<in T>
    {
        Domain.Validators.ValidationResult Validate(T request);
    }
}
