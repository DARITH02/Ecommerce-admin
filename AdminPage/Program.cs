using AdminPage.Data;
//using Microsoft.EntityFrameworkCore;
using System.Configuration;
//using DbContext = AdminPage.Data.DbContext;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<DbContext>();
builder.Services.AddTransient<ProductsDbContext>();
builder.Services.AddTransient<InventoryDbContext>();
builder.Services.AddTransient<ColorDbContext>();
builder.Services.AddTransient<DetailPhoneDbContext>();
builder.Services.AddTransient<PcDbContext>();
builder.Services.AddTransient<CountDbContext>();

/*builder.Services.AddDbContext<ApplicationDbContex>(Options => Options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection")
    ));*/

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddDbContext<ProductsDbContext>(options =>
//        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
//}
