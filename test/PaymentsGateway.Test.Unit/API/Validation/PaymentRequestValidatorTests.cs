using FluentAssertions;
using PaymentGateway.Api.Models.Requests;
using PaymentsGateway.Api.Validation;
using Xunit;

namespace PaymentsGateway.Test.Unit.API.Validation
{
    public class PaymentRequestValidatorTests
    {
        [Fact]
        public void Validate_ValidPaymentRequest_RetrunsSuccess()
        {
            // Arrange
            var request = new PostPaymentRequest("1234567898765432", 10, 2028, "GBP",10,"123");

            var sut = new PaymentRequestValidator();

            // Act
            var result = sut.Validate(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Validate_InValidPaymentRequest_RetrunsFailure()
        {
            // Arrange
            var request = new PostPaymentRequest(string.Empty, 10, 2028, "GBP", 10, "123");

            var sut = new PaymentRequestValidator();

            // Act
            var result = sut.Validate(request);

            // Assert
            result.IsFailure.Should().BeTrue();
        }
    }
}
