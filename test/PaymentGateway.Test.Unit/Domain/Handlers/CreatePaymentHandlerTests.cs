﻿using FluentAssertions;
using Moq;
using PaymentGateway.Domain.Handlers;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;
using PaymentGateway.Domain.Ports;
using PaymentGateway.Infrastructure;

using Xunit;

namespace PaymentGateway.Test.Unit.Domain.Handlers
{
    public class CreatePaymentHandlerTests
    {
        private readonly CreatePaymentHandler _sut;
        private readonly Mock<ICreatePaymentCommand> _createPaymentCommandMock = new();

        public CreatePaymentHandlerTests()
        {
            _sut = new CreatePaymentHandler(_createPaymentCommandMock.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldRetrunInternalError_WhenCreatePaymentFailed()
        {
            // Arrange
            _createPaymentCommandMock.Setup(x => x.CreatePayment(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new CreatePaymentResponse.InternalError());

            var paymentRequest = CreatePaymentRequest();

            // Act
            var result = await _sut.HandleAsync(paymentRequest);

            // Assert
            result.IsInternalError.Should().BeTrue();
            _createPaymentCommandMock.Verify(d => d.CreatePayment(It.IsAny<CreatePaymentRequest>()), Times.Once());
        }

        [Fact]
        public async Task HandleAsync_ShouldRetrunSuccess_WhenCreatePaymentSuccess()
        {
            // Arrange
            _createPaymentCommandMock.Setup(x => x.CreatePayment(It.IsAny<CreatePaymentRequest>()))
                .ReturnsAsync(() => new CreatePaymentResponse.Success(new PaymentDetails(Guid.NewGuid(),
                PaymentStatus.Authorized,
                "1234567891234567",
                Currency.GBP,
                10,
                2028,
                200)));

            var paymentRequest = CreatePaymentRequest();

            // Act
            var result = await _sut.HandleAsync(paymentRequest);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _createPaymentCommandMock.Verify(d => d.CreatePayment(It.IsAny<CreatePaymentRequest>()), Times.Once());
        }

        private CreatePaymentRequest CreatePaymentRequest()
        {
            return new CreatePaymentRequest("1111111111111111", 10, 2023, 10, Currency.GBP, "123");
        }
    }
}