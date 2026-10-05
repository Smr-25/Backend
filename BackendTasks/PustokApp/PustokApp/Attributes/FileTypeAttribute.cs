using System.ComponentModel.DataAnnotations;

namespace PustokApp.Attributes;

public class FileTypeAttribute:ValidationAttribute
{
    private readonly string[] _allowedTypes;
    public FileTypeAttribute(params string[] allowedTypes)
    {
        _allowedTypes = allowedTypes;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var listFiles = new List<IFormFile>();
        if (value is IFormFile file)
        {
            listFiles.Add(file);
        }
        if (value is IFormFile[] files)
        {
            listFiles.AddRange(files);
        }
        if (listFiles != null)
        {
            foreach (var f in listFiles)
            {
                if (!_allowedTypes.Contains(f.ContentType))
                {
                    return new ValidationResult($"File type must be one of the following: {string.Join(", ", _allowedTypes)}");
                }
            }
        }
        return ValidationResult.Success;
    }
}
