using HRManagement.API.Mapping;
using HRManagement.Data;
using HRManagement.Data.Repository.IRepository;
using HRManagement.Data.Repository;
using Microsoft.EntityFrameworkCore;
using HRManagement.Web.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapHub<NotificationHub>("/notificationHub");

// 1. Önce area'larý tanýmla
// 1. Önce area'larý tanýmla
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// 2. Root / boþ URL geldiðinde => Admin/Dashboard/Index'e yönlendir
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}",
    defaults: new { area = "Admin" });

// 3. EN SON fallback tanýmý
app.MapFallbackToAreaController("Index", "User", "Admin"); // En sona al


app.Run();


