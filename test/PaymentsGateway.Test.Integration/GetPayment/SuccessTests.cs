using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Newtonsoft.Json;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentsGateway.Test.Integration.Fixtures;
using Xunit;

namespace PaymentsGateway.Test.Integration.GetPayment
{
    public class SuccessTests : TestServerFixture
    {

        [Fact]
        public async Task GetPayment_ShouldReturnSuccess_WhenPaymentIdValid()
        {
            // Arrange
            var client = Server.CreateClient();

            var postPaymentRequest = new PostPaymentRequest(
                CardNumber: "2222405343248877",
                ExpiryMonth: 4,
                ExpiryYear: 2025,
                Cvv: "123",
                Currency: "GBP",
                Amount: 100);

            var paymentResponse = await client.PostAsJsonAsync("/payments", postPaymentRequest);
            var body = await paymentResponse.Content.ReadAsStringAsync();
            var paymentResult = JsonConvert.DeserializeObject<PostPaymentResponse>(body);

            // Act
            var response = await client.GetAsync($"/payments/{paymentResult?.Id}");
            var getPaymentResponse = await response.Content.ReadFromJsonAsync<GetPaymentResponse>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            getPaymentResponse!.Id.Should().NotBeEmpty();
            getPaymentResponse!.Status.Should().Be("Authorized");
            getPaymentResponse.CardNumberLastFour.Should().Be(postPaymentRequest!.CardNumber[^4..]);
            getPaymentResponse.ExpiryMonth.Should().Be(postPaymentRequest.ExpiryMonth);
            getPaymentResponse.ExpiryYear.Should().Be(postPaymentRequest.ExpiryYear);
            getPaymentResponse.Currency.Should().Be(postPaymentRequest.Currency);
            getPaymentResponse.Amount.Should().Be(postPaymentRequest.Amount);
        }
    }
}