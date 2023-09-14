using CrudWithGeocoding.Models;
using CrudWithGeocoding.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI for the DbContext
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IGeocodingService, GeocodingService>(sp => new GeocodingService("AIzaSyDvZAscT76jYQm_nFAfcfUn30As6DHmoRU"));
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
