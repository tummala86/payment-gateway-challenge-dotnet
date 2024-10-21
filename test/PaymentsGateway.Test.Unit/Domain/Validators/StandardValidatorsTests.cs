using FluentAssertions;
using PaymentsGateway.Domain.Validators;
using Xunit;

namespace PaymentsGateway.Test.Unit.Domain.Validators
{
    public class StandardValidatorsTests
    {
        [Theory]
        [InlineData("field1", "A-value", true)]
        [InlineData("field1", null, false)]
        public void Should_Validate_Required_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateRequired(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("field1", "07b1fed9-f612-4d04-bd96-ab92e551de19", true)]
        [InlineData("field1", "test", false)]
        public void Should_Validate_Uuid_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateUuid(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("CardNumber", "1234567898765432", true)]
        [InlineData("CardNumber", "12", false)]
        [InlineData("CardNumber", "", false)]
        public void Should_Validate_Card_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateCardNumber(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("Currency", "GBP", true)]
        [InlineData("Currency", "GB", false)]
        [InlineData("Currency", "", false)]
        public void Should_Validate_Currency_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateCurrency(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("Cvv", "123", true)]
        [InlineData("Cvv", "12", false)]
        [InlineData("Cvv", "", false)]
        public void Should_Validate_Cvv_Field(string fieldName, string? value, bool isValid)
        {
            StandardValidators.ValidateCvv(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("ExpiryMonth", 10,2027, true)]
        [InlineData("ExpiryMonth", 10, 2024, true)]
        [InlineData("ExpiryMonth", 5, 2022, false)]
        [InlineData("ExpiryMonth", 8, 2023, false)]
        [InlineData("ExpiryMonth", 0, 0, false)]
        public void Should_Validate_ExpiryMonth_Field(string fieldName, int month, int year, bool isValid)
        {
            StandardValidators.ValidateMonth(fieldName, month, year).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("ExpiryYear", 2027, true)]
        [InlineData("ExpiryYear", 2024, true)]
        [InlineData("ExpiryYear", 2022, false)]
        [InlineData("ExpiryYear", 2023, false)]
        [InlineData("ExpiryYear", 0, false)]
        public void Should_Validate_ExpiryYear_Field(string fieldName, int year, bool isValid)
        {
            StandardValidators.ValidateYear(fieldName, year).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("Amount", 10, true)]
        [InlineData("Amount", 0, false)]
        public void Should_Validate_Amount_Field(string fieldName, int value, bool isValid)
        {
            StandardValidators.ValidateAmount(fieldName, value).IsSuccess.Should().Be(isValid);
        }
    }
}
