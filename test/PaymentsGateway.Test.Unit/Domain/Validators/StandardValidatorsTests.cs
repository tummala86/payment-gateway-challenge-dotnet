using System;

using FluentAssertions;

using Moq;

using PaymentsGateway.Domain.Validators;
using Xunit;

namespace PaymentsGateway.Test.Unit.Domain.Validators
{
    public class StandardValidatorsTests
    {
        private readonly Mock<TimeProvider> _timeProvider;

        public StandardValidatorsTests()
        {
            _timeProvider = new Mock<TimeProvider>();
        }


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
        public void Should_Validate_Card_Field(string fieldName, string value, bool isValid)
        {
            StandardValidators.ValidateCardNumber(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("Currency", "GBP", true)]
        [InlineData("Currency", "GB", false)]
        [InlineData("Currency", "", false)]
        public void Should_Validate_Currency_Field(string fieldName, string value, bool isValid)
        {
            StandardValidators.ValidateCurrency(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("Cvv", "123", true)]
        [InlineData("Cvv", "12", false)]
        [InlineData("Cvv", "", false)]
        public void Should_Validate_Cvv_Field(string fieldName, string value, bool isValid)
        {
            StandardValidators.ValidateCvv(fieldName, value).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("ExpiryMonth", 10, true)]
        [InlineData("ExpiryMonth", 0, false)]
        public void Should_Validate_ExpiryMonth_Field(string fieldName, int month, bool isValid)
        {
            StandardValidators.ValidateExpiryMonth(fieldName, month).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("ExpiryYear", 5, 2027, true)]
        [InlineData("ExpiryYear", 10, 2024, true)]
        [InlineData("ExpiryYear", 9, 2024, false)]
        [InlineData("ExpiryYear", 1, 2022, false)]
        [InlineData("ExpiryYear", 6, 2023, false)]
        [InlineData("ExpiryYear", 0, 0, false)]
        public void Should_Validate_ExpiryYear_Field(string fieldName, int month, int year, bool isValid)
        {
            _timeProvider.Setup(x => x.GetUtcNow()).Returns(DateTime.UtcNow);

            StandardValidators.ValidateExpiryYear(fieldName, month, year, _timeProvider.Object).IsSuccess.Should().Be(isValid);
        }

        [Theory]
        [InlineData("ExpiryYear", 1, 2028, 8, 2024, 31, true)]
        [InlineData("ExpiryYear", 10, 2024, 10, 2024, 31, true)]
        [InlineData("ExpiryYear", 5, 2022, 05, 2024, 30, false)]
        public void Should_Validate_ExpiryDate(string fieldName, int expiryMonth, int expiryYear,
            int currentMonth, int currentYear, int days, bool isValid)
        {
            _timeProvider.Setup(x => x.GetUtcNow()).Returns(new DateTime(currentYear,currentMonth, days));

            StandardValidators.ValidateExpiryDate(fieldName, expiryMonth, expiryYear, _timeProvider.Object).IsSuccess.Should().Be(isValid);
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
