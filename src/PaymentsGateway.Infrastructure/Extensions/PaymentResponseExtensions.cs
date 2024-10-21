using PaymentsGateway.Domain.Models;
using PaymentsGateway.Infrastructure.Database.Entities;

namespace PaymentsGateway.Infrastructure.Extensions
{
    public static class PaymentResponseExtensions
    {
        public static PaymentDetails ToDomainPaymentDetails(this Payment payment)
            => new(
                payment.Id,
                payment.Status,
                payment.CardNumber,
                payment.Currency,
                payment.ExpiryMonth,
                payment.ExpiryYear,
                payment.Amount);
    }
}
