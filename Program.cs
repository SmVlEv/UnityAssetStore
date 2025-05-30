using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UnityAssetStore.Data;
using UnityAssetStore.Models.Identity;
using UnityAssetStore.Services;

var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ��������� DI

// 1. ����������� � SQL Server ����� EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Identity: ���������� ApplicationUser � IdentityRole
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// 3. ��������� Session (���� ���!)
builder.Services.AddDistributedMemoryCache(); // ��������� ��� Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = ".UnityAssetStore.Cart";
    options.Cookie.IsEssential = true;
});

// 4. MVC
builder.Services.AddControllersWithViews();

// 5. ����������� ���������������� ��������
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// ������������ middleware

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot
app.UseRouting();

// �������������� � �����������
app.UseAuthentication(); // ������ ���� ������������ Identity
app.UseAuthorization();

// �������� Session Middleware
app.UseSession(); // <-- ����� UseRouting()

// ��������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Areas (�������)
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.Run();