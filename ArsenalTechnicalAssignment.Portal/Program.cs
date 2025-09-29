using ArsenalTechnicalAssignment.Data.Data;
using ArsenalTechnicalAssignment.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", optional: true)
    .Build();
var connectionString = Environment.GetEnvironmentVariable("ArsenalConnectionString");
builder.Services.AddScoped((s) => new SqlContext(connectionString ?? ""));
builder.Services.AddScoped<ISQLSyncService, SQLSyncService>();
builder.Services.AddScoped((s) => new ExternalIntegrationService("https://api.football-data.org/v4/"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
