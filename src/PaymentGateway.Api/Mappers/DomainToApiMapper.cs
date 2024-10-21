using PaymentGateway.Api.Models.Responses;
using PaymentsGateway.Domain.Models.Enum;
using DomainPaymentDetails = PaymentsGateway.Domain.Models.PaymentDetails;

namespace PaymentsGateway.Api.Mappers
{
    public static class DomainToApiMapper
    {
        public static GetPaymentResponse ToApiResponse(this DomainPaymentDetails response)
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
            PaymentStatus.Failed => "Failed",
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
}
