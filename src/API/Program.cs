using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;
using System.Reflection;

namespace API
{
    internal class Program
    {
        private static IConfiguration Configuration { get; } = BuildConfiguration();

        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureSwagger(builder);
            ConfigureLogging();
            ConfigureServices(builder);
            AddServices(builder);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                     .AllowAnyMethod()
                                     .AllowAnyHeader());
            });

            var app = builder.Build();

            ConfigureMiddleware(app);

            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<VendaDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));



            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<IVendaRepository, VendaRepository>();
            builder.Services.AddScoped<IVendaService, VendaService>();
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static async void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
        }

        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VendaHub API",
                    Version = "v1",
                    Description = "A VendaHub API é responsável por gerenciar e processar operações relacionadas a vendas, como criação, alteração, consulta e cancelamento de vendas. Ela também publica eventos relacionados ao ciclo de vida das vendas.",
                    Contact = new OpenApiContact()
                    {
                        Name = "Squad de Vendas",
                        Email = "suporte@123vendas.com.br",
                    },
                    Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        {
                            "x-logo", new OpenApiObject
                            {
                                { "url", new OpenApiString("https://cursosnovaalianca.com/wp-content/uploads/2023/10/plane_venda.jpg") },
                                { "altText", new OpenApiString("VendaHub API") }
                            }
                        }
                    }
                });

                // Inclui comentários XML para documentação via Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        private static void ConfigureLogging()
        {
            var minLogLevel = Serilog.Events.LogEventLevel.Information;

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(minLogLevel)
                .WriteTo.Console(formatter: new JsonFormatter())
                .WriteTo.File("logs/vendahub_log.json", rollingInterval: RollingInterval.Day);
            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (!app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllOrigins");
            app.MapControllers();
        }
    }
}