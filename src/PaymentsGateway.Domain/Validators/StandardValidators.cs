using System.Text.RegularExpressions;

namespace PaymentsGateway.Domain.Validators
{
    public static class StandardValidators
    {
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

        public static ValidationResult ValidateCurrency(string fieldName, string value)
            => value.Length != 3
            ? StandardParameterErrors.InvalidCurrencyValue(fieldName)
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

        public static ValidationResult ValidateMonth(string fieldName, int? value)
        {
            return value is not null && (value >= 1 && value <= 12)
           ? new ValidationResult.Success()
           : StandardParameterErrors.InvalidMonthValue(fieldName);
        }

        public static ValidationResult ValidateYear(string fieldName, int? value, TimeProvider timeProvider)
        {
            var currentDate = timeProvider.GetUtcNow();

            return value is not null && (value >= currentDate.Year)
                ? new ValidationResult.Success()
                : StandardParameterErrors.InvalidYearValue(fieldName);
        }

        public static ValidationResult ValidateExpiryDate(string fieldName, int monthValue, int yearValue, TimeProvider timeProvider)
        {
            var currentDate = timeProvider.GetUtcNow().Date;
            var daysInMonth = DateTime.DaysInMonth(yearValue, monthValue);
            var expiryDate = new DateTime(yearValue, monthValue, daysInMonth).Date;

            return (expiryDate >= currentDate)
                ? new ValidationResult.Success()
                : StandardParameterErrors.InvalidMonthValue(fieldName);
        }

    }
}
