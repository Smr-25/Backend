namespace FirstApiApp.Helpers;

public static class FileManager
{
    public static string SaveFile(this IFormFile file, string folderPath)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);
        return fileName;
    }
    
    public static bool CheckFileType(this IFormFile file)
    {
        return !file.ContentType.Contains("image/");
    }
    public static bool CheckFileSize(this IFormFile file, int sizeInMb)
    {
        return file.Length >= sizeInMb * 1024 * 1024;
    }
    
    public static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}