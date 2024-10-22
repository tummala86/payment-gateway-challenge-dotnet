using OneOf;

namespace PaymentsGateway.Infrastructure.ExternalServices.Models;

[GenerateOneOf]
public partial class PaymentResult : OneOfBase<
    PaymentResult.Success,
    PaymentResult.Error>
{
    public record Success(bool Authorized, string AuthorizationCode);
    public record Error();

    public bool IsSuccess => IsT0;
    public Success AsSuccess => AsT0;

    public bool IsError => IsT1;
    public Error AsError => AsT1;
}