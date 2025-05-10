using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using HRMS.UI.Areas.Identity;
using HRMS.UI.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using HRMS.UI.Services;
using HRMS.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

// Configuration reference
var config = builder.Configuration;

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Razor + Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// External Authentication Providers
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        var googleSection = config.GetSection("Authentication:Google");
        options.ClientId = googleSection["ClientId"];
        options.ClientSecret = googleSection["ClientSecret"];
        options.CallbackPath = "/signin-google";
    });


// Scoped services
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("✅ Database connection successful.");
        }
        else
        {
            Console.WriteLine("❌ Database connection failed (CanConnect returned false).");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Failed to connect to the database:");
        Console.WriteLine(ex.Message);
    }
}

app.Run();
