using PaymentsGateway.Api.Constants;

namespace PaymentsGateway.Test.Integration.Utils
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddIdempotencyKeyHeader(this HttpClient httpClient, string value)
        {
            httpClient.DefaultRequestHeaders.Add(ApiHeaders.IdempotencyKey, value);
            return httpClient;
        }
    }
}
