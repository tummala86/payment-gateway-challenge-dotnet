using FluentAssertions;
using Newtonsoft.Json;
using PaymentGateway.Api.Models.Responses;
using PaymentsGateway.Test.Integration.Fixtures;
using PaymentsGateway.Test.Integration.Utils;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace PaymentsGateway.Test.Integration.GetPayment
{
    public class SuccessTests : TestServerFixture
    {

        [Fact]
        public async Task GetPayment_ShouldReturnSuccess_WhenPaymentIdValid()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var merchantId = Guid.NewGuid().ToString();
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


            var paymentResponse = await client.PostAsJsonAsync("/payments", paymentRequest);
            var body = await paymentResponse.Content.ReadAsStringAsync();
            var paymentResult = JsonConvert.DeserializeObject<PostPaymentResponse>(body);

            // Act
            var response = await client.GetAsync($"/payments/{paymentResult?.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getPaymentPayload = await response.Content.ReadAsStringAsync();
            getPaymentPayload.Should().NotBeNull();
        }
    }
}
