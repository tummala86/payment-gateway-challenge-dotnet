using System.Text.Json.Serialization;

namespace PaymentGateway.Infrastructure.ExternalServices.Models;

public class PaymentRequest
{
    [JsonPropertyName("card_number")]
    public string CardNumber { get; init; } = null!;

    [JsonPropertyName("expiry_date")]
    public string ExpiryDate { get; init; } = null!;

    [JsonPropertyName("cvv")]
    public string Cvv { get; init; } = null!;

    [JsonPropertyName("currency")]
    public string Currency { get; init; } = null!;

    [JsonPropertyName("amount")]
    public int Amount { get; init; }
}