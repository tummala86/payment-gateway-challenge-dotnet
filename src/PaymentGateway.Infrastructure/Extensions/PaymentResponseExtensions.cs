using PaymentGateway.Domain.Models;
using PaymentGateway.Infrastructure.Database.Entities;

namespace PaymentGateway.Infrastructure.Extensions;

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