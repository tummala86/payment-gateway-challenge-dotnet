using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;

namespace PaymentGateway.Api.Extensions;

public static class CreatePaymentRequestExtensions
{
    public static CreatePaymentRequest ToDomain(this PostPaymentRequest request)
    => new(
            request.CardNumber!,
            request.ExpiryMonth,
            request.ExpiryYear,
            request.Amount,
            request.Currency.ToCurrency(),
            request.Cvv
        );

    private static Currency ToCurrency(this string currency)
        => currency.ToUpper() switch
        {
            "GBP" => Currency.GBP,
            "USD" => Currency.USD,
            "EUR" => Currency.EUR,
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
        };
}