using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models;

var builder = WebApplication.CreateBuilder(args);

// Tilf�j DbContext og konfiguration for Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))); // S�rg for at denne connection string findes i appsettings.json

// Konfigurer Identity med brugerdefineret DbContext
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders(); // Standard token providers til ting som password reset

// Tilf�j MVC og controller views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfigurer HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Developer exception page til fejlfindingsform�l i udvikling
}
else
{
    app.UseExceptionHandler("/Home/Error"); // H�ndtering af fejl i produktion
    app.UseHsts(); // HTTP Strict Transport Security (kun i produktion)
}

app.UseHttpsRedirection(); // Force HTTPS for sikkerhed
app.UseStaticFiles(); // G�r statiske filer tilg�ngelige (som CSS, JS, billeder)

app.UseRouting(); // Brug routing middleware til at h�ndtere requests

app.UseAuthentication();  // S�rg for at authentication middleware er tilf�jet
app.UseAuthorization();   // S�rg for at authorization middleware er tilf�jet

// Konfigurere controller routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
