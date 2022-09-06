using System.ComponentModel.DataAnnotations;

namespace WBlog.Shared.Attribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ArrayStringLengthAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; }
        public int MaximumLength { get; }

        public ArrayStringLengthAttribute(int maximumLength)
        {
            MaximumLength = maximumLength;
            ErrorMessage = $"The number of tags should be in the range from {MinimumLength} to {MaximumLength}";
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
}