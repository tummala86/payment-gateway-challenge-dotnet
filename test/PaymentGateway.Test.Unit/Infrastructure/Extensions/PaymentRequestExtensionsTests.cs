﻿using FluentAssertions;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;
using PaymentGateway.Infrastructure.Database.Extensions;

using Xunit;

namespace PaymentGateway.Test.Unit.Infrastructure.Extensions;

public class PaymentRequestExtensionsTests
{
    [Fact]
    public void CreatePaymentRequest_ShouldMapToBankPaymentRequest()
    {
        // Arrange
        var paymentId = Guid.NewGuid();
        var bankAuthCode = Guid.NewGuid().ToString();

        var createPaymentRequest = new CreatePaymentRequest(
            CardNumber: "1234567898765432",
            ExpiryMonth: 10,
            ExpiryYear: 2028,
            Amount: 100,
            Currency.GBP,
            Cvv: "123");

        // Act
        var result = createPaymentRequest.ToPaymentRequest();

        // Assert
        result.CardNumber.Should().Be(createPaymentRequest.CardNumber);
        result.Amount.Should().Be(createPaymentRequest.Amount);
        result.ExpiryDate.Should().Be($"{createPaymentRequest.ExpiryMonth:D2}/{createPaymentRequest.ExpiryYear}");
        result.Currency.Should().Be(Currency.GBP.ToString());
        result.Cvv.Should().Be(createPaymentRequest.Cvv);
    }
}