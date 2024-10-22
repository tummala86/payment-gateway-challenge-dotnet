using FluentAssertions;
using PaymentsGateway.Test.Integration.Fixtures;
using PaymentsGateway.Test.Integration.Utils;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PaymentsGateway.Test.Integration.CreatePayment
{
    public class FailureTests : TestServerFixture
    {
        [Fact]
        public async Task ShouldReturn_InvalidParameters_WhenRequestIsInvalid()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var client = Server.CreateClient()
                .AddIdempotencyKeyHeader(idempotencyKey);

            var paymentRequest = PaymentRequest(0);

            // Act
            var response = await client.PostAsJsonAsync("/payments", paymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturn_BadRequestError_WhenIdempotencyKeyHeaderNotPresent()
        {
            // Arrange
            var client = Server.CreateClient();

            var paymentRequest = PaymentRequest(100);

            // Act
            var response = await client.PostAsJsonAsync("/Payments", paymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturn_ReqestUnprocessedError_WhenIdempotencyKeyReused()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var client = Server.CreateClient().AddIdempotencyKeyHeader(idempotencyKey);

            var paymentRequest1 = PaymentRequest(100);
            var paymentRequest2 = PaymentRequest(200);

            // Act
            var response1 = await client.PostAsJsonAsync("/payments", paymentRequest1);
            var response2 = await client.PostAsJsonAsync("/payments", paymentRequest2);

            // Assert
            response1.StatusCode.Should().Be(HttpStatusCode.Created);
            response2.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            var body = await response2.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        private object PaymentRequest(int amount)
        {
            return new
            {
                card_number = "2222405343248877",
                expiry_month = 4,
                expiry_year = 2025,
                currency = "gbp",
                amount = amount,
                cvv = "123"
            };
        }
    }
}
