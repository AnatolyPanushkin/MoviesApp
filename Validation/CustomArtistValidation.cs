using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace MoviesApp.Validation
{
    public class CustomArtistValidation:ValidationAttribute
    {
        private readonly int lenght;

        public CustomArtistValidation(int lenght)
        {
            this.lenght = lenght;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString()?.Length < 4) return new ValidationResult("the lenght of name is invalid!");
            return ValidationResult.Success;
        }
    }
    
}