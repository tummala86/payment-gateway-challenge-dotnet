using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PaymentGateway.Test.Integration.Fixtures;
using Xunit;

namespace PaymentGateway.Test.Integration.CreatePayment
{
    public class FailureTests : TestServerFixture
    {
        [Fact]
        public async Task CreatePayment_ShouldReturnInvalidParameters_WhenRequestIsInvalid()
        {
            // Arrange
            var client = Server.CreateClient();

            var postPaymentRequest = new
            {
                cardNumber = "2222405343248877",
                expiryMonth = 4,
                expiryYear = 2025,
                currency = "GBP",
                amount = 0,
                cvv = "123"
            };

            // Act
            var response = await client.PostAsJsonAsync("/payments", postPaymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }
    }
}