using AppSettingsMultiPlatformPackage;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppSettingsMultiPlatformJson(builder, "Mac");
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();