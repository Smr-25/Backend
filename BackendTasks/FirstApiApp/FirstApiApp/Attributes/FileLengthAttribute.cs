using System.ComponentModel.DataAnnotations;

namespace FirstApiApp.Attributes;

public class FileLengthAttribute : ValidationAttribute
{
    private readonly long _maxLength;
    public FileLengthAttribute(long maxLength)
    {
        _maxLength = maxLength;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            if (file.Length > _maxLength)
            {
                return new ValidationResult($"File size must be less than {_maxLength / (1024 * 1024)} MB.");
            }
        }
        return ValidationResult.Success;
    }
}