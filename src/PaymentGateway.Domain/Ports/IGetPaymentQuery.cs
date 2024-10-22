using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Ports;

public interface IGetPaymentQuery
{
    Task<GetPaymentResponse> GetPayment(GetPaymentRequest request);
}