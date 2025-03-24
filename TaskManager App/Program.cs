using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskManager_App.Data;
using TaskManager_App.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManager_AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManager_AppContext") ?? throw new InvalidOperationException("Connection string 'TaskManager_AppContext' not found.")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManager_AppContext") ?? throw new InvalidOperationException("Connection string 'TaskManagerContext' not found.")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to true if email confirmation is needed
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); ;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await seedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acount}/{action=Login}/{id?}")
    .WithStaticAssets();


 app.Run();
