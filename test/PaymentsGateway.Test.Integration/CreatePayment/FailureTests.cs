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
        public async Task ShouldReturn_InvalidParameters_WehnRequestIsInvalid()
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
        public async Task ShouldReturn_InvalidIdempotencyKey_WhenIdempotencyKeyHeaderNotPresent()
        {
            // Arrange
            var client = Server.CreateClient();

            var paymentRequest = PaymentRequest(20);

            // Act
            var response = await client.PostAsJsonAsync("/Payments", paymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturn_IdempotencyKeyReuseError()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var client = Server.CreateClient().AddIdempotencyKeyHeader(idempotencyKey);

            var paymentRequest1 = PaymentRequest(10);
            var paymentRequest2 = PaymentRequest(20);

            // Act
            var response1 = await client.PostAsJsonAsync("/payments", paymentRequest1);
            var response2 = await client.PostAsJsonAsync("/payments", paymentRequest2);

            // Assert
            response1.StatusCode.Should().Be(HttpStatusCode.Accepted);
            response2.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            var body = await response2.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }

        private object PaymentRequest(decimal amount)
        {
            return new
            {
                card_number = "1111111111111111",
                expiry_month = 10,
                expiry_year = 2028,
                currency = "gbp",
                amount = amount,
                cvv = "123"
            };
        }
    }
}
