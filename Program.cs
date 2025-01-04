using Microsoft.EntityFrameworkCore;
using StarMediaGroupTest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Add DbContext with SQL Server connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    // Other cookie settings can be configured here
});

// Build the application
var app = builder.Build();

// Middleware configuration
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles();      // Serve static files like CSS, JS, and images
app.UseRouting();          // Enable routing

// Authorization middleware
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pages}/{action=Home}/{id?}"
);

// Run the application
app.Run();

