using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager_App.Data;
using TaskManager_App.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TaskManager_AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManager_AppContext") ?? throw new InvalidOperationException("Connection string 'TaskManager_AppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    seedData.Initialize(services);
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TaskItems}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
