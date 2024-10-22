using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PaymentsGateway.Infrastructure.ExternalServices.Models;

namespace PaymentsGateway.Infrastructure.ExternalServices;

public class AcquiringBankClient : IAcquiringBankClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AcquiringBankClient> _logger;

    public AcquiringBankClient(HttpClient httpClient, ILogger<AcquiringBankClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PaymentResult> ProcessPayment(PaymentRequest request)
    {
        try
        {
            using var httpResponse = await _httpClient.PostAsJsonAsync("/payments", request, new JsonSerializerOptions());

            var content = await httpResponse.Content.ReadAsStringAsync();

            if (httpResponse.IsSuccessStatusCode)
            {
                var paymentResponse = JsonSerializer.Deserialize<PaymentResponse>(content);

                return paymentResponse is null
                ? new PaymentResult.Error()
                : new PaymentResult.Success(
                    paymentResponse.Authorized,
                    paymentResponse.AuthorizationCode);
            }

            _logger.LogInformation($"There is an error while processing payment : {content}");
            return new PaymentResult.Error();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There is an error while processing payment");
            return new PaymentResult.Error();
        }
    }
}