using Microsoft.EntityFrameworkCore;
using ZeneApp.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DB Context hozzáadása
builder.Services.AddDbContext<ZeneContext>(options =>
    options.UseSqlite("Data Source=../zeneapp.db")); // Közös adatbázis fájl

var app = builder.Build();

// ... (szabványos konfiguráció)
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Zene}/{action=Index}/{id?}");

app.Run();