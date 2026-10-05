using System.ComponentModel.DataAnnotations;

namespace FirstApiApp.Attributes;

public class FileTypeAttribute: ValidationAttribute
{
    private readonly string[] _allowedFileTypes;

    public FileTypeAttribute(params string[] allowedFileTypes)
    {
        _allowedFileTypes = allowedFileTypes;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            if (!_allowedFileTypes.Contains(file.ContentType))
            {
                return new ValidationResult($"Invalid file type. Allowed types are: {string.Join(", ", _allowedFileTypes)}.");
            }
        }
        return ValidationResult.Success;
    }
}