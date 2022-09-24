using System.ComponentModel.DataAnnotations;

namespace WBlog.Shared.Attribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class ArrayStringLengthAttribute : ValidationAttribute
{
    public int MinimumLength { get; private set; }
    public int MaximumLength { get; private set; }

    public ArrayStringLengthAttribute(int minimumLength, int maximumLength, string? errorMessage = null)
    {
        MaximumLength = maximumLength;
        MinimumLength = minimumLength;
        ErrorMessage = string.IsNullOrEmpty(errorMessage)
            ? $"The number of tags should be in the range from {MinimumLength} to {MaximumLength}"
            : errorMessage;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var temp = GetCollection<string>(value);
        if (temp == null)
            return null;
        if (temp.Count() < MinimumLength || temp.Count() > MaximumLength)
            return new ValidationResult(ErrorMessage);
        return ValidationResult.Success;
    }

    private IEnumerable<T>? GetCollection<T>(object? value)
    {
        return value as IEnumerable<T>;
    }
}