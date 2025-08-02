using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Data;
using SmartEyewearStore.Services;
using SmartEyewearStore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<EnsureGuestIdFilter>();
});

builder.Services.AddSession();

builder.Services.AddScoped<SmartEyewearStore.Services.InteractionService>();
builder.Services.AddScoped<ContentBasedService>();
builder.Services.AddScoped<CollaborativeFilteringService>();
builder.Services.AddScoped<HybridRecommendationService>();

builder.Services.AddHttpContextAccessor(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
