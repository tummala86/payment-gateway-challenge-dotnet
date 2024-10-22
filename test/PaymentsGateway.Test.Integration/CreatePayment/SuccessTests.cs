using FluentAssertions;
using PaymentsGateway.Test.Integration.Fixtures;
using PaymentsGateway.Test.Integration.Utils;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PaymentsGateway.Test.Integration.CreatePayment
{
    public class SuccessTests : TestServerFixture
    {
        [Fact]
        public async Task CreatePayment_ShouldReturn_PaymentAuthorized()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var client = Server.CreateClient().AddIdempotencyKeyHeader(idempotencyKey);

            var paymentRequest = new
            {
                card_number = "2222405343248877",
                expiry_month = 4,
                expiry_year = 2025,
                currency = "gbp",
                amount = 100,
                cvv = "123"
            };

            // Act
            var response = await client.PostAsJsonAsync("/payments", paymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }
    }
}
