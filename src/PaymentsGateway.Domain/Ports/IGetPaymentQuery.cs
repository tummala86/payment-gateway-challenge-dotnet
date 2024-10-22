using PaymentsGateway.Domain.Models;

namespace PaymentsGateway.Domain.Ports;

public interface IGetPaymentQuery
{
    Task<GetPaymentResponse> GetPayment(GetPaymentRequest request);
}
