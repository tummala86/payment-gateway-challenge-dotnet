using PaymentsGateway.Domain.Models;

namespace PaymentsGateway.Domain.Ports
{
    public interface ICreatePaymentCommand
    {
        Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request);
    }
}
