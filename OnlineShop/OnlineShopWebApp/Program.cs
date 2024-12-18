using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShop.SecretManager.Services;
using OnlineShopWebApp.Services;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Загрузка переменных окружения из .env файла в конфигурацию
var configuration = new ConfigurationBuilder()
    .AddDotNetEnv("app.env", LoadOptions.TraversePath())
    .Build();

builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.WithProperty("ApplicationName", "Masterskaya Veloservice"));

// Добавление сервисов в DI-контейнер
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<CartService>();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddTransient<MarkdownService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<ComparisonService>();
builder.Services.AddScoped<IComparisonRepository, ComparisonRepository>();
builder.Services.AddTransient<BikePartsService>();
builder.Services.AddTransient<ServicesService>();
builder.Services.AddTransient<FavoritesService>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddTransient<RoleService>();
builder.Services.AddTransient<UserService>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// SecretManager
var secrets = await builder.Services.GetApplicationSecretsAsync(configuration);

if (string.IsNullOrEmpty(secrets.ConnectionString) ||
    string.IsNullOrEmpty(secrets.DefaultAdminEmail) ||
    string.IsNullOrEmpty(secrets.DefaultAdminPassword))
{
    throw new ArgumentNullException("Получены пустые значения секретов из SecretManager");
}

// Добавляем контекст DatabaseContext в качестве сервиса в приложение
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(secrets.ConnectionString));

// Указываем тип пользователя и роли
builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    // Устанавливаем тип хранилища - наш контекст
    .AddEntityFrameworkStores<DatabaseContext>()
    // Добавляем провайдер токенов
    .AddDefaultTokenProviders();

// Настройка cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = "/account/authorization/login";
    options.LogoutPath = "/account/authorization/logout";
    options.Cookie = new CookieBuilder
    {
        IsEssential = true
    };
});

// Для использования HttpContext вне контроллеров
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        var originalPath = context.Request.Path.Value;
        context.Items["originalPath"] = originalPath;
        context.Request.Path = "/error/pageNotFound";
        await next();
    }
});

app.UseRouting();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "Ошибка при подключении к БД");
    }

    // Инициализация администратора
    var userManager = services.GetRequiredService<UserManager<User>>();
    var rolesManager = services.GetRequiredService<RoleManager<Role>>();
    await IdentityInitializer.Initialize(userManager, rolesManager, secrets);
}

// Подключение аутентификации
app.UseAuthentication();

// Подключение авторизации
app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
