using FlightManagement.Application.Services;
using FlightManagement.Infrastructure;
using FlightManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Flight}/{action=Index}/{id?}");
});
app.MapControllers();

app.Run();
