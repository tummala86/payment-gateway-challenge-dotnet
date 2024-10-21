using PaymentsGateway.Infrastructure.ExternalServices.Models;

namespace PaymentsGateway.Infrastructure.ExternalServices;

public interface IAcquiringBankClient
{
    Task<PaymentResults> ProcessPayment(PaymentRequest request);
}
