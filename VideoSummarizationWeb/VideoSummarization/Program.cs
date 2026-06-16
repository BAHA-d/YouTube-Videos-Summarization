using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VideoSummarization.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connStr = builder.Configuration.GetConnectionString("DCS");
builder.Services.AddDbContext<VideoSummDBContext>(option => option.UseSqlServer(connStr));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(x => x.LoginPath = "/Accounts/Login");
builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
    options.AddPolicy("All", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
