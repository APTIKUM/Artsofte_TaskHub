using Api.Infrastructure.DependencyInjectionDemo.Interfaces;
using Api.Infrastructure.DependencyInjectionDemo.Services;
using Api.Middlewares;
using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Dal;
using Logic;
using Microsoft.OpenApi.Models;

namespace Api;

/// <summary>
/// Конфигурация приложения
/// </summary>
public sealed class Startup
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Окружение приложения
    /// </summary>
    private IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public void ConfigureServices(IServiceCollection services)
    {

        // Singleton
        services.AddSingleton<ISingletonService1, SingletonService1>();
        services.AddSingleton<ISingletonService2, SingletonService2>();

        // Scoped
        services.AddScoped<IScopedService1, ScopedService1>();
        services.AddScoped<IScopedService2, ScopedService2>();

        // Transient
        services.AddTransient<ITransientService1, TransientService1>();
        services.AddTransient<ITransientService2, TransientService2>();

        services.AddControllers();
        services.AddDal();
        services.AddLogic();
        
        services.AddScoped<IManageUserUseCase, ManageUserUseCase>();
        
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskHub Api",
                Version = "v1"
            });
        });
    }

    /// <summary>
    /// Конфигурация middleware пайплайна
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskHub API v1");
            });
        }

        app.UseMiddleware<ResponseTimeMiddleware>();

        app.Use(async (context, next) =>
        {
            context.Response.OnStarting(() =>
            {
                // кириллица не работает , нужно ascii
                var student = "Cherkasov Vladislav Dimitrievich"; 
                var group = "RI-240949";

                context.Response.Headers["X-Student-Name"] = student;
                context.Response.Headers["X-Student-Group"] = group;

                return Task.CompletedTask;
            });

            await next();
        });

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}