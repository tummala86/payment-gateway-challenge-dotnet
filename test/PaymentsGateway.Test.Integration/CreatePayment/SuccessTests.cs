using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentsGateway.Test.Integration.Fixtures;
using Xunit;

namespace PaymentsGateway.Test.Integration.CreatePayment
{
    public class SuccessTests : TestServerFixture
    {
        [Theory]
        [MemberData(nameof(PostPaymentRequest))]
        public async Task CreatePayment_ShouldReturnSuccess(PostPaymentRequest postPaymentRequest, string expectedPaymentStatus)
        {
            // Arrange
            var client = Server.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/payments", postPaymentRequest);
            var paymentResponse = await response.Content.ReadFromJsonAsync<PostPaymentResponse>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            paymentResponse!.Id.Should().NotBeEmpty();
            paymentResponse!.Status.Should().Be(expectedPaymentStatus);
            paymentResponse.CardNumberLastFour.Should().Be(postPaymentRequest!.CardNumber[^4..]);
            paymentResponse.ExpiryMonth.Should().Be(postPaymentRequest.ExpiryMonth);
            paymentResponse.ExpiryYear.Should().Be(postPaymentRequest.ExpiryYear);
            paymentResponse.Currency.Should().Be(postPaymentRequest.Currency);
            paymentResponse.Amount.Should().Be(postPaymentRequest.Amount);
        }

        public static IEnumerable<object[]> PostPaymentRequest => new List<object[]>
        {
            new object[] {
                new PostPaymentRequest(
                CardNumber: "2222405343248877",
                ExpiryMonth: 4,
                ExpiryYear: 2025,
                Cvv: "123",
                Currency: "GBP",
                Amount: 100),
                "Authorized"},
            new object[] {
                new PostPaymentRequest(
                CardNumber: "2222405343248112",
                ExpiryMonth: 1,
                ExpiryYear: 2026,
                Cvv: "456",
                Currency: "USD",
                Amount: 60000),
                "Declined"}
        };
    }
}