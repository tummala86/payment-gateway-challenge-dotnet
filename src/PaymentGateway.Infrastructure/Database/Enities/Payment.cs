using PaymentGateway.Domain.Models.Enum;

namespace PaymentGateway.Infrastructure.Database.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public string CardNumber { get; set; } = null!;
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string Cvv { get; set; } = null!;
    public int Amount { get; set; }
    public Currency Currency { get; set; }
    public PaymentStatus Status { get; set; }
    public string? BankAuthorizationCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}