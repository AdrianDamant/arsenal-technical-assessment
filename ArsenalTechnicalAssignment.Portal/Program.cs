using ArsenalTechnicalAssignment.Data.Data;
using ArsenalTechnicalAssignment.Data.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
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
        }
        catch
        {
            //Failed - Database is not available

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(app =>
                    {
                        app.Run(async context =>
                        {
                            context.Response.ContentType = "text/html";
                            await context.Response.WriteAsync($@"
                    <html>
                        <head>
                            <title>Startup Error</title>
                            <style>
                                body {{
                                    background-color: #fff;
                                    font-family: 'Segoe UI', sans-serif;
                                    color: #212121;
                                    margin: 0;
                                    padding: 0;
                                }}
                                .container {{
                                    max-width: 600px;
                                    margin: 80px auto;
                                    padding: 40px;
                                    border: 2px solid #e41b17;
                                    border-radius: 12px;
                                    box-shadow: 0 0 10px rgba(0,0,0,0.1);
                                }}
                                h1 {{
                                    color: #e41b17;
                                    font-size: 28px;
                                    margin-bottom: 20px;
                                    border-bottom: 2px solid #e41b17;
                                    padding-bottom: 10px;
                                }}
                                pre {{
                                    background-color: #f9f9f9;
                                    padding: 15px;
                                    border-left: 4px solid #e41b17;
                                    white-space: pre-wrap;
                                    word-wrap: break-word;
                                }}
                                .badge {{
                                    display: inline-block;
                                    background-color: #e41b17;
                                    color: white;
                                    padding: 6px 12px;
                                    border-radius: 4px;
                                    font-weight: bold;
                                    margin-bottom: 20px;
                                }}
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <div class='badge'>Arsenal Technical Assessment</div>
<h3>Adrian Damant</h3>
                                <h1>Startup Failure</h1>
                                <pre>The database is asleep! click F5 (perhaps a couple of times) to wake it up.
It is a Dev Azure DB instance and goes to sleep when not in use.</pre>
                            </div>
                        </body>
                    </html>");
                        });
                    });
                })
                .Build()
                .Run();
        }
    }
}