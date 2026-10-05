using Microsoft.AspNetCore.Hosting;

namespace MiniAppApi.Utils;

public class FileManager(IWebHostEnvironment environment)
{
    public async Task<string> SaveEventBannerAsync(int eventId, IFormFile file)
    {
        return await SaveImageAsync(file, $"uploads/events/{eventId}");
    }

    public async Task<string> SaveOrganizerLogoAsync(int organizerId, IFormFile file)
    {
        return await SaveImageAsync(file, $"uploads/organizers/{organizerId}");
    }

    private async Task<string> SaveImageAsync(IFormFile file, string relativeFolder)
    {
        var folderPath = Path.Combine(environment.WebRootPath, relativeFolder);
        Directory.CreateDirectory(folderPath);
        var existingFile = Directory
            .GetFiles(folderPath)
            .FirstOrDefault();

        if (existingFile != null)
        {
            File.Delete(existingFile);
        }
        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"{relativeFolder}/{fileName}".Replace("\\", "/");
    }
}