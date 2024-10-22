using PaymentGateway.Api.Models.Requests;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Models.Enum;

namespace PaymentsGateway.Api.Extensions;

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
