namespace PaymentsGateway.Domain.Validators
{
    public record ParameterError(string ParameterName, string ErrorMessage);

    public static class StandardParameterErrors
    {
        public static ParameterError RequestBodyRequired(string fieldName) => new(fieldName, "Request body is required.");

        public static ParameterError Required(string fieldName) => new(fieldName, "Value is required.");

        public static ParameterError InvalidUuidValue(string fieldName)
            => new(fieldName, "Value provided doesn't match a valid uuid.");

        public static ParameterError InvalidCurrencyValue(string fieldName)
            => new(fieldName, "Value must be 3 characters long.");

        public static ParameterError InvalidCardValue(string fieldName)
            => new(fieldName, "Value must be a 14-19 digit string.");

        public static ParameterError InvalidCvvValue(string fieldName)
            => new(fieldName, "Value must be a 3-4 digit string containing only digits.");

        public static ParameterError InvalidAmoutValue(string fieldName)
            => new(fieldName, "Value is required and must be greater than 0.");

        public static ParameterError InvalidMonthValue(string fieldName)
            => new(fieldName, "Value must be a future month and between 1 to 12.");

        public static ParameterError InvalidYearValue(string fieldName)
            => new(fieldName, "Value must be greater than or equal to current year");
    }

    public static class ParameterErrorExtensions
    {
        public static ParameterError WithPrefix(this ParameterError parameterError, string prefix)
            => new(string.IsNullOrEmpty(parameterError.ParameterName) ? prefix : $"{prefix}.{parameterError.ParameterName}", parameterError.ErrorMessage);


        public static Dictionary<string, string[]> GetGroupedErrors(this IEnumerable<ParameterError> errors)
            => errors.GroupBy(x => x.ParameterName)
                .ToDictionary(
                   x => x.Key,
                   x => x.Select(e => e.ErrorMessage).ToArray());
    }
}
