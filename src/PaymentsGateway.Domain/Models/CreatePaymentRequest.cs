using PaymentsGateway.Domain.Models.Enum;

namespace PaymentsGateway.Domain.Models
{
    public record CreatePaymentRequest(
        int Amount,
        Currency Currency,
        string CardNumber, 
        int ExpiryMonth, 
        int ExpiryYear, 
        string Cvv);
}