using PaymentGateway.Domain.Models.Enum;

namespace PaymentGateway.Domain.Models;

public record PaymentDetails(
    Guid Id,
    PaymentStatus Status,
    string CardNumber,
    Currency Currency,
    int ExpiryMonth,
    int ExpiryYear,
    int Amount);