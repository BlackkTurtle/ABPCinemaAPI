using System;
using System.ComponentModel.DataAnnotations;

namespace ABPCinemaAPI.DAL.Validators
{

    /// <summary>
    /// Compares current DateTime property against another DateTime property on the same DTO.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GreaterThanPropertyAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public GreaterThanPropertyAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime currentValue)
            {
                return ValidationResult.Success;
            }

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
            {
                return new ValidationResult($"Property '{_comparisonProperty}' was not found.");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue is DateTime startValue && currentValue <= startValue)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be after {_comparisonProperty}.");
            }

            return ValidationResult.Success;
        }
    }
}