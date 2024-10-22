using System.Net;
using FluentAssertions;
using PaymentGateway.Test.Integration.Fixtures;
using Xunit;

namespace PaymentGateway.Test.Integration.GetPayment
{
    public class FailureTests : TestServerFixture
    {
        [Fact]
        public async Task GetPayment_ShouldReturnNotFound_WhenPaymentIdDoesnotExist()
        {
            // Arrange
            var client = Server.CreateClient();
            var paymentId = Guid.NewGuid();

            // Act
            var response = await client.GetAsync($"/payments/{paymentId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }
    }
}