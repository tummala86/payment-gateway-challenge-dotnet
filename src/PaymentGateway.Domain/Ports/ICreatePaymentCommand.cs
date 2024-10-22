using PaymentGateway.Domain.Models;

namespace PaymentGateway.Domain.Ports;

public interface ICreatePaymentCommand
{
    Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request);
}