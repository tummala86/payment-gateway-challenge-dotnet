using PaymentsGateway.Infrastructure.ExternalServices.Models;

namespace PaymentsGateway.Infrastructure.ExternalServices;

public interface IAcquiringBankClient
{
    Task<PaymentResult> ProcessPayment(PaymentRequest request);
}
