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
        public async Task Should_Return_Payment_Accepted()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var client = Server.CreateClient().AddIdempotencyKeyHeader(idempotencyKey);

            var paymentRequest = new
            {
                card_number = "1111111111111111",
                expiry_month = 10,
                expiry_year = 2028,
                currency = "gbp",
                amount = 20,
                cvv = "123"
            };

            // Act
            var response = await client.PostAsJsonAsync("/payments", paymentRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().NotBeNull();
        }
    }
}
