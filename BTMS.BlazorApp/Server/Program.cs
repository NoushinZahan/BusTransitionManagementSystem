using BTMS.BlazorApp.Shared.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BusDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db"), ma => ma.MigrationsAssembly("BTMS.BlazorApp.Server")));
#region
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200", "http://localhost:44452")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
        });
});
#endregion
#region AddNewtonsoftJson 
builder.Services.AddControllers().AddNewtonsoftJson(
    settings =>
    {
        settings.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        settings.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
    }
    );
#endregion
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
app.UseBlazorFrameworkFiles();
app.UseCors("EnableCORS");

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
