using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BYU_EGYPT_INTEX.Models;
using Microsoft.ML.OnnxRuntime;
using BYU_EGYPT_INTEX.Areas.Identity.Data;
using BYU_EGYPT_INTEX.Core.Repo;
using BYU_EGYPT_INTEX.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add user database to the container.
var authConnectString = builder.Configuration["ConnectionStrings:AuthLink"];
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(authConnectString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//Connect Database:
var conectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<egyptbyuContext>(options =>
    options.UseNpgsql(conectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    // Require better passwords
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
})
    .AddRoles<IdentityRole>() //Add role service
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

AddScoped();

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
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; font-src 'self' fonts.gstatic.com; style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; script-src 'self' 'unsafe-inline'; img-src 'self'");
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

void AddScoped()
{
    builder.Services.AddScoped<IUserRepo, UserRepo>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IRoleRepo, RoleRepo>();
}