using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BYU_EGYPT_INTEX.Data;
using BYU_EGYPT_INTEX.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authConnectString = builder.Configuration["ConnectionStrings:AuthLink"];
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(authConnectString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();



//var AppconnectionString = builder.Configuration.GetConnectionString("DefaultAppConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite(AppconnectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//Connect Database:
var conectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<egyptbyuContext>(options =>
    options.UseNpgsql(conectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//CSP Header, allow bootstrap from CDN
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; font-src 'self' fonts.gstatic.com; style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; script-src 'self'; img-src 'self'");
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

