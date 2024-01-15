var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services for SPA static files
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "clientapp/build"; // Path to your React app build
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseSpaStaticFiles(); // Serve the static files of the SPA

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure the app to serve the SPA's index.html for any unknown paths
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp"; // Path to your React app source

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:3000"); // URL of the React development server
    }
});

app.Run();
