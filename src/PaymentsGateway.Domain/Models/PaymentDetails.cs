using PaymentsGateway.Domain.Models.Enum;

namespace PaymentsGateway.Domain.Models;

public record PaymentDetails(
    Guid Id,
    PaymentStatus Status,
    string CardNumber,
    Currency Currency,
    int ExpiryMonth,
    int ExpiryYear,
    int Amount);