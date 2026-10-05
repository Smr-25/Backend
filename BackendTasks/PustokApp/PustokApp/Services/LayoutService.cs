

using PustokApp.Data;

namespace PustokApp.Services;

public class LayoutService(AppDbContext db)
{
    public Dictionary<string,string> GetSettings()
    {
        return db.Settings.ToDictionary(s => s.Key, s => s.Value);
    }
}
