using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Models.Enum;
using PaymentsGateway.Infrastructure.Database.Entities;
using PaymentsGateway.Infrastructure.ExternalServices.Models;

namespace PaymentsGateway.Infrastructure.Database.Extensions
{
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
}
