using OneOf;

namespace PaymentsGateway.Domain.Validators;

[GenerateOneOf]
public partial class ValidationResult : OneOfBase<ValidationResult.Success, ValidationResult.Failure>
{
    public static implicit operator ValidationResult(ParameterError error) => new Failure(new[] { error });
    public static implicit operator ValidationResult(ParameterError[] errors) => new Failure(errors);

    public static ValidationResult Succeed() => new Success();
    public static ValidationResult Fail(params ParameterError[] errors) => new Failure(errors);

    public static ValidationResult Combine(params ValidationResult[] results)
    {
        var errors = new List<ParameterError>();

        foreach (var result in results)
        {
            if (result.TryPickT1(out var failure, out _))
                errors.AddRange(failure.Errors);
        }
        return errors.Any() ? new Failure(errors) : new Success();
    }

    public record Success;
    public record Failure(IEnumerable<ParameterError> Errors);

    public bool IsSuccess => IsT0;
    public bool IsFailure => IsT1;

    public Success AsSuccess => AsT0;
    public Failure AsFailure => AsT1;
}

public static class ValidationResultExtension
{
    public static ValidationResult ContinueIfSuccess(this ValidationResult result, Func<ValidationResult> next)
        => result.Match(success => next(), failure => failure);

    public static Dictionary<string, string[]> GetGroupErrors(this ValidationResult result)
        => result.Match(
            success => throw new InvalidOperationException(),
            failure => failure.Errors.GetGroupedErrors());
}