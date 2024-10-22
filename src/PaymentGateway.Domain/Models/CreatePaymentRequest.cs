using PaymentGateway.Domain.Models.Enum;

namespace PaymentGateway.Domain.Models
{
    public record CreatePaymentRequest(
        string CardNumber,
        int ExpiryMonth,
        int ExpiryYear,
        int Amount,
        Currency Currency,
        string Cvv);
}