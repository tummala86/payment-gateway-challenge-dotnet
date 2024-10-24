﻿using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Domain.Models.Enum;
using DomainPaymentDetails = PaymentGateway.Domain.Models.PaymentDetails;

namespace PaymentGateway.Api.Mappers;

public static class DomainToApiMapper
{
    public static PostPaymentResponse ToPostPaymentResponse(this DomainPaymentDetails response)
        => new(
            response.Id,
            response.Status.ToApiStatus(),
            response.CardNumber[^4..],
            response.ExpiryMonth,
            response.ExpiryYear,
            response.Currency.ToApiCurrency(),
            response.Amount);

    public static GetPaymentResponse ToGetPaymentResponse(this DomainPaymentDetails response)
        => new(
            response.Id,
            response.Status.ToApiStatus(),
            response.CardNumber[^4..],
            response.ExpiryMonth,
            response.ExpiryYear,
            response.Currency.ToApiCurrency(),
            response.Amount);

    private static string ToApiStatus(this PaymentStatus status)
    => status switch
    {
        PaymentStatus.Authorized => "Authorized",
        PaymentStatus.Declined => "Declined",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };

    private static string ToApiCurrency(this Currency currency)
    => currency switch
    {
        Currency.GBP => "GBP",
        Currency.EUR => "EUR",
        Currency.USD => "USD",
        _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
    };
}