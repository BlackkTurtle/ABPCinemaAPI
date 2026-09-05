using System;
using System.ComponentModel.DataAnnotations;

namespace ABPCinemaAPI.DAL.Validators
{
    /// <summary>
    /// Validates that a DateTime is on a whole hour and within operating bounds.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OperatingHourAttribute : ValidationAttribute
    {
        public int MinHour { get; set; } = 6;
        public int MaxHour { get; set; } = 23;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime dateTime)
            {
                return ValidationResult.Success;
            }

            // Check if minutes, seconds, and milliseconds are zero
            if (dateTime.TimeOfDay.Minutes != 0 ||
                dateTime.TimeOfDay.Seconds != 0 ||
                dateTime.TimeOfDay.Milliseconds != 0)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be on whole hours (00 minutes/seconds).");
            }

            // Allow 00:00 (midnight) if it represents the end of operating day (24:00)
            int hour = dateTime.Hour;
            bool isMidnightEnd = (hour == 0 && dateTime.TimeOfDay == TimeSpan.Zero);

            if (!isMidnightEnd && (hour < MinHour || hour > MaxHour))
            {
                return new ValidationResult($"{validationContext.DisplayName} must be between {MinHour}:00 and {MaxHour}:00.");
            }

            return ValidationResult.Success;
        }
    }
}