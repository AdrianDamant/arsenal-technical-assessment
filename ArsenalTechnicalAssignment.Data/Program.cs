using ArsenalTechnicalAssignment.Data.Data;
using ArsenalTechnicalAssignment.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", optional: true)
    .Build();
var connectionString = Environment.GetEnvironmentVariable("ArsenalConnectionString");
builder.Services.AddScoped((s) => new SqlContext(connectionString??""));
builder.Services.AddScoped<ISQLSyncService, SQLSyncService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//As this is a technical excersize I am commenting out this logic so we can use Swagger evn once deployed
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
