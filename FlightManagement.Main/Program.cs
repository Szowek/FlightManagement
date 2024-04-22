using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FlightManagement.Application.Services;
using FlightManagement.Infrastructure;
using FlightManagement.Infrastructure.Repositories;
using FlightManagement.Main.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AccountDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AccountDbContextConnection' not found.");

builder.Services.AddDbContext<AccountDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<AccountMainUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AccountDbContext>();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<SeedService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var seedService = services.GetRequiredService<SeedService>();
        await seedService.SeedDatabase();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error occurred while seeding database: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Flight}/{action=Index}/{id?}");

app.Run();
