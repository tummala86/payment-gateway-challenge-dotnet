using OneOf;

namespace PaymentGateway.Domain.Models;

[GenerateOneOf]
public partial class GetPaymentResponse : OneOfBase<GetPaymentResponse.Success,
      GetPaymentResponse.NotFound,
      GetPaymentResponse.InternalError>
{
    public record Success(PaymentDetails PaymentDetails);

    public record NotFound();

    public record InternalError();

    public bool IsSuccess => IsT0;
    public Success AsSuccess => AsT0;

    public bool IsNotFound => IsT1;
    public NotFound AsNotFound => AsT1;

    public bool IsInternalError => IsT2;
    public InternalError AsInternalError => AsT2;
}