using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PaymentsGateway.Test.Integration.Fixtures;
using Xunit;

namespace PaymentsGateway.Test.Integration.CreatePayment
{
    public class FailureTests : TestServerFixture
    {
        [Fact]
        public async Task CreatePayment_ShouldReturnInvalidParameters_WhenRequestIsInvalid()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var client = Server.CreateClient();

            var paymentRequest = PaymentRequest(0);

            // Act
            var response = await client.PostAsJsonAsync("/payments", paymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
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