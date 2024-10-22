using System.Text.Json.Serialization;

namespace PaymentGateway.Infrastructure.ExternalServices.Models;

public class PaymentResponse
{
    [JsonPropertyName("authorized")]
    public bool Authorized { get; init; }

    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; init; } = null!;
}