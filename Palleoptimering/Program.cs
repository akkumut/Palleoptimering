using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"), sqlOptions =>
        sqlOptions.EnableRetryOnFailure()));  // Aktiverer retry ved fejl i forbindelse med SQL Server

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

app.UseStaticFiles(); // G�r statiske filer tilg�ngelige (som CSS, JS, billeder)

app.UseRouting(); // Brug routing middleware til at h�ndtere requests

app.UseAuthentication();  // S�rg for at authentication middleware er tilf�jet
app.UseAuthorization();   // S�rg for at authorization middleware er tilf�jet

// Konfigurere controller routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");


app.Run();
