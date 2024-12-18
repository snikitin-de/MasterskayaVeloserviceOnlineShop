
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories;
using OnlineShop.SecretManager.Services;
using OnlineShopWebApi.Middleware;
using OnlineShopWebApi.Services;
using OnlineShopWebApp.Services;
using System.Text;

namespace OnlineShopWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Загрузка переменных окружения из .env файла в конфигурацию
            var configuration = new ConfigurationBuilder()
                .AddDotNetEnv("app.env", LoadOptions.TraversePath())
                .Build();

            builder.Configuration.AddJsonFile("appsettings.WebApi.json", optional: false, reloadOnChange: true);

            // Добавление сервисов в DI-контейнер
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                {
                    // Добавляем, чтобы не было ошибки с сериализацией ссылочных моделей
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    // Добавляем конвертер для Enum
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                }
            );
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<UserService>();
            builder.Services.AddTransient<CartService>();
            builder.Services.AddScoped<ICartsRepository, CartsRepository>();
            builder.Services.AddTransient<ProductService>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
            builder.Services.AddHttpContextAccessor();

            // SecretManager
            var secrets = await builder.Services.GetApplicationSecretsAsync(configuration);

            if (string.IsNullOrEmpty(secrets.ConnectionString) ||
                string.IsNullOrEmpty(secrets.JwtKey) ||
                string.IsNullOrEmpty(secrets.JwtIssuer) ||
                string.IsNullOrEmpty(secrets.JwtAudience))
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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.UseInlineDefinitionsForEnums();

                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Online shop API",
                    Description = "Online shop ASP.NET Core Web API"
                });

                // Включаем авторизацию используя Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = secrets.JwtIssuer,
                    ValidAudience = secrets.JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrets.JwtKey))
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online shop API v1");
                    c.RoutePrefix = "swagger";
                    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                });
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<JWTMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
