using FluentAssertions;
using PaymentsGateway.Api.Validation;

using Xunit;

namespace PaymentsGateway.Test.Unit.API.Validation
{
    public class GetPaymentRequestValidatorTests
    {
        [Fact]
        public void Validate_ValidRequestParameters_RetrunsSuccess()
        {
            // Act
            var result = GetPaymentRequestValidator.Validate(Guid.NewGuid());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
