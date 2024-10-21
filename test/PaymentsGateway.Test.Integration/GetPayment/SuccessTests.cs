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
        public async Task GetPayment_ShouldReturnNotFound_WhenPaymentIdValid()
        {
            // Arrange
            var idempotencyKey = Guid.NewGuid().ToString();
            var merchantId = Guid.NewGuid().ToString();
            var client = Server.CreateClient().AddIdempotencyKeyHeader(idempotencyKey);

            var paymentRequest = new
            {
                merchant_id = merchantId,
                referrence = "test",
                amount = 20,
                currency = "gbp",
                card = new
                {
                    number = "1111111111111111",
                    expiry_month = 10,
                    expiry_year = 2028,
                    cvv = "123"
                }
            };


            var paymentResponse = await client.PostAsJsonAsync("/payments", paymentRequest);
            var body = await paymentResponse.Content.ReadAsStringAsync();
            var paymentResult = JsonConvert.DeserializeObject<PostPaymentResponse>(body);

            // Act
            var response = await client.GetAsync($"/v1/payments/{paymentResult?.Id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getPaymentPayload = await response.Content.ReadAsStringAsync();
            getPaymentPayload.Should().NotBeNull();
        }
    }
}
