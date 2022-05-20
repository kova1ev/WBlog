using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace WBlog.Shared.Attribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ArrayStringLengthAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; }
        public int MaximunLength { get; set; }
        public ArrayStringLengthAttribute(int maximumLength)
        {
            MaximunLength = maximumLength;
            ErrorMessage = $"The number of tags should be in the range from {MinimumLength} to {MaximunLength}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var temp = GetCollection<string>(value);
            if (temp == null)
                return null;
            if (temp.Count() < MinimumLength || temp.Count() > MaximunLength)
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }

        private IEnumerable<T>? GetCollection<T>(object? value)
        {
            return value as IEnumerable<T>;
        }

    }
}