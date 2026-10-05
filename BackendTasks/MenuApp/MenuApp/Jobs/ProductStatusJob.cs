using MenuApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MenuApp.Jobs;

public class ProductStatusJob(AppDbContext appDbContext)
{
    public string JobId { get; set; } = Guid.NewGuid().ToString();
    
    public string CronExpression { get; set; } = "0 0 * * *";

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
       appDbContext.Products
           .Where(p=>p.IsNew && EF.Functions.DateDiffMinute(p.CreatedAt, DateTime.UtcNow) >= 5)
           .ToList()
           .ForEach(p=>p.IsNew = false);
       return appDbContext.SaveChangesAsync(cancellationToken);
    }
}