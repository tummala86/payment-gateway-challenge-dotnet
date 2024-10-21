namespace PaymentGateway.Api.Models.Responses;

public record GetPaymentResponse(
    Guid Id, 
    string Status, 
    string CardNumber, 
    int ExpiryMonth,
    int ExpiryYear,
    string Currency, 
    int Amount);