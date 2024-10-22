using PaymentGateway.Domain.Models;
using PaymentGateway.Infrastructure.ExternalServices.Models;

namespace PaymentGateway.Infrastructure.Database.Extensions;

public static class PaymentRequestExtensions
{
    public static PaymentRequest ToPaymentRequest(this CreatePaymentRequest request)
       => new()
       {
           CardNumber = request.CardNumber,
           ExpiryDate = $"{request.ExpiryMonth:D2}/{request.ExpiryYear}",
           Currency = request.Currency.ToString(),
           Amount = request.Amount,
           Cvv = request.Cvv
       };
}