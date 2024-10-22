using PaymentGateway.Infrastructure.ExternalServices.Models;

namespace PaymentGateway.Infrastructure.ExternalServices;

public interface IAcquiringBankClient
{
    Task<PaymentResult> ProcessPayment(PaymentRequest request);
}