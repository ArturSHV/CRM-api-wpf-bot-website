using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApp.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddRazorPages();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();


app.UseStatusCodePagesWithRedirects("/NotFound/");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" 
    );

app.MapDefaultControllerRoute();

app.MapRazorPages();



app.Run();
