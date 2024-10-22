using OneOf;

namespace PaymentsGateway.Domain.Models;

[GenerateOneOf]
public partial class CreatePaymentResponse : OneOfBase<CreatePaymentResponse.Success, CreatePaymentResponse.InternalError>
{
    public record Success(PaymentDetails PaymentDetails);
    public record InternalError();

    public bool IsSuccess => IsT0;
    public Success AsSuccess => AsT0;

    public bool IsInternalError => IsT1;
    public InternalError AsInternalError => AsT1;
}