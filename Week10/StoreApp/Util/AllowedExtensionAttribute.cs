using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace StoreApp.Util
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {
        private readonly string _extension;

        public AllowedExtensionAttribute(string extension) => _extension = extension;

        protected override ValidationResult IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            if (value is null)
                return ValidationResult.Success!;

            if (value is not IFormFile file)
                return new ValidationResult("This is not a file.");

            var extension = Path.GetExtension(file.FileName).ToLower();

            return _extension != extension
                ? new ValidationResult("This Photo has invalid format.")
                : ValidationResult.Success!;
        }
    }
}
