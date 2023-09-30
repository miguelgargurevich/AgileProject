//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AgileProject.Data;
using AgileProject.Repository.Contratos;
using AgileProject.Repository.Implementaciones;
using AgileProject.Services.Contratos;
using AgileProject.Services.Implementaciones;
using AutoMapper;
using AgileProject.Entidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.AddScoped<IClaimsServices, ClaimsServices>();
builder.Services.AddScoped<ICalendarServices, CalendarServices>();
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("dbDesa"); //DefaultConnection
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); // Agregar el proveedor de registro en la consola
});

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<AspNetUsers>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();


// Start Registering and Initializing AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// End Registering and Initializing AutoMapper

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Cuenta/Login";
    option.Cookie.Name = "my_app_auth_cookie";
});

var app = builder.Build();

//MFG
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

