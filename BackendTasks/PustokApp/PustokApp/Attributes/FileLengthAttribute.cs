using System.ComponentModel.DataAnnotations;

namespace PustokApp.Attributes;

public class FileLengthAttribute:ValidationAttribute
{
    private readonly long _maxLength;
    public FileLengthAttribute(long maxLength)
    {
        _maxLength = maxLength;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var listFiles=new List<IFormFile>();
        if (value is IFormFile file)
        {
            listFiles.Add(file);
        }
         if (value is IFormFile[] files)
        {
            listFiles.AddRange(files);
        }
        if (listFiles is not null)
        {
            foreach (var f in listFiles)
            {
                if (f.Length > _maxLength)
                {
                    return new ValidationResult($"File size must not exceed {_maxLength} bytes.");
                }
            }
        }
        return ValidationResult.Success;
    }
}
