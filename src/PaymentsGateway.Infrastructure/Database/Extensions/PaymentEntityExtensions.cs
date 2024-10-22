using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Models.Enum;
using PaymentsGateway.Infrastructure.Database.Entities;

namespace PaymentsGateway.Infrastructure.Database.Extensions;

public static class PaymentEntityExtensions
{
    public static Payment ToPaymentEntity(this CreatePaymentRequest request, Guid Id)
   => new()
   {
       Id = Id,
       CardNumber = request.CardNumber,
       ExpiryMonth = request.ExpiryMonth,
       ExpiryYear = request.ExpiryYear,
       Cvv = request.Cvv,
       Amount = request.Amount,
       Currency = request.Currency,
       Status = PaymentStatus.Initiated,
       CreatedAt = DateTime.UtcNow
   };
}