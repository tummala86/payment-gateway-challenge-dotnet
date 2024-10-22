using PaymentsGateway.Domain.Models.Enum;

namespace PaymentsGateway.Domain.Models
{
    public record CreatePaymentRequest(
        string CardNumber, 
        int ExpiryMonth, 
        int ExpiryYear,
        int Amount,
        Currency Currency,
        string Cvv);
}