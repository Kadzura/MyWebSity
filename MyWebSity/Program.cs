// Program.cs
// Web
using System.Net;
using System.Text.Json;
// Геолакация
using MaxMind.GeoIP2;
// Классы
using MyWebSity.Models;
using MyWebSity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Connection String from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register ISqlUserService with SqlConnection
builder.Services.AddScoped<ISqlUserService>(provider => new SqlUserService(connectionString));


// Add GeoIpService
builder.Services.AddTransient<GeoIpService>();

// Add HttpClientFactory
builder.Services.AddHttpClient();

// Add session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession(); // Add session middleware

// Important: No auth middleware
//app.UseAuthentication();
//app.UseAuthorization();

// Custom routing
app.MapControllerRoute(
    name: "home",
    pattern: "/",
    defaults: new { controller = "Home", action = "Index" }
);
app.MapControllerRoute(
    name: "login",
    pattern: "/login",
    defaults: new { controller = "Account", action = "Login" }
);
app.MapControllerRoute(
    name: "logout",
    pattern: "/logout",
    defaults: new { controller = "Account", action = "Logout" }
);
app.MapControllerRoute(
    name: "profile",
    pattern: "/profile",
    defaults: new { controller = "Profile", action = "Index" }
);

app.Run();