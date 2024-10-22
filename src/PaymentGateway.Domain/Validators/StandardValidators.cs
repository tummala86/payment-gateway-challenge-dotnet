using System.Text.RegularExpressions;

namespace PaymentGateway.Domain.Validators;

public static class StandardValidators
{
    private static readonly string[] ValidCurrencies = ["USD", "EUR", "GBP"];

    public static ValidationResult ValidateRequired(string fieldName, string? value)
        => string.IsNullOrWhiteSpace(value)
        ? StandardParameterErrors.Required(fieldName)
        : new ValidationResult.Success();

    public static ValidationResult ValidateRequired(string fieldName, int? value)
       => value is null
       ? StandardParameterErrors.Required(fieldName)
       : new ValidationResult.Success();

    public static ValidationResult ValidateUuid(string fieldName, string? value)
        => !Guid.TryParse(value, out var uuid)
        ? StandardParameterErrors.InvalidUuidValue(fieldName)
        : new ValidationResult.Success();

    public static ValidationResult ValidateCurrencyValue(string fieldName, string value)
        => value.Length != 3
        ? StandardParameterErrors.InvalidCurrencyValue(fieldName)
        : new ValidationResult.Success();

    public static ValidationResult ValidateCurrency(string fieldName, string value)
        => value.Length == 3 && !ValidCurrencies.Contains(value)
        ? StandardParameterErrors.InvalidCurrency(fieldName)
        : new ValidationResult.Success();

    public static ValidationResult ValidateAmount(string fieldName, int value)
        => value <= 0
        ? StandardParameterErrors.InvalidAmoutValue(fieldName)
        : new ValidationResult.Success();

    public static ValidationResult ValidateCardNumber(string fieldName, string value)
    {
        Regex regex = new("^[0-9]{14,19}$");
        return !regex.Match(value).Success
                ? StandardParameterErrors.InvalidCardValue(fieldName)
                : new ValidationResult.Success();
    }

    public static ValidationResult ValidateCvv(string fieldName, string value)
    {
        Regex regex = new("^[0-9]{3,4}$");
        return !regex.Match(value).Success
                ? StandardParameterErrors.InvalidCvvValue(fieldName)
                : new ValidationResult.Success();
    }

    public static ValidationResult ValidateExpiryMonth(string fieldName, int value)
    {
        return value >= 1 && value <= 12
                ? new ValidationResult.Success()
                : StandardParameterErrors.InvalidMonthValue(fieldName);
    }

    public static ValidationResult ValidateExpiryYear(string fieldName, int month, int year, TimeProvider timeProvider)
    {
        var currentDate = timeProvider.GetUtcNow();

        bool isValidYearAndMonth = year > currentDate.Year || (year == currentDate.Year && month >= currentDate.Month);

        return isValidYearAndMonth
            ? new ValidationResult.Success()
            : StandardParameterErrors.InvalidExpiryMonthAndYearValue(fieldName);
    }

    public static ValidationResult ValidateExpiryDate(string fieldName, int monthValue, int yearValue, TimeProvider timeProvider)
    {
        var currentDate = timeProvider.GetUtcNow().Date;
        var daysInMonth = DateTime.DaysInMonth(yearValue, monthValue);
        var expiryDate = new DateTime(yearValue, monthValue, daysInMonth).Date;

        return (expiryDate >= currentDate)
            ? new ValidationResult.Success()
            : StandardParameterErrors.InvalidExpiryMonthAndYearValue(fieldName);
    }
}