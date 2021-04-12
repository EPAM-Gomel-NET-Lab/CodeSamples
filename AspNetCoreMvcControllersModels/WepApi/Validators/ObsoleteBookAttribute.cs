using System;
using System.ComponentModel.DataAnnotations;

namespace WepApi.Validators
{
    public class ObsoleteBookAttribute : ValidationAttribute
    {
        public ObsoleteBookAttribute(int year) => Year = year;

        public int Year { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var releaseYear = ((DateTime)value).Year;

            if (releaseYear < Year)
            {
                return new ValidationResult($"Book should be released no later than {Year}.");
            }

            return ValidationResult.Success;
        }
    }
}
