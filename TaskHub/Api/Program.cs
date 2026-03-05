using Api.Infrastructure.DependencyInjectionDemo.Extensions;
using Api.Infrastructure.DependencyInjectionDemo.Interfaces;
using LoggingLibrary;

namespace Api;

/// <summary>
/// Точка входа приложения
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Запуск приложения
    /// </summary>
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .UseInfraSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .Build();

        using (var scope1 = host.Services.CreateScope())
        {
            Console.WriteLine("===== SCOPE 1 =====");

            var provider = scope1.ServiceProvider;

            provider.ResolveTwice<ISingletonService1>();
            provider.ResolveTwice<ISingletonService2>();

            provider.ResolveTwice<IScopedService1>();
            provider.ResolveTwice<IScopedService2>();

            provider.ResolveTwice<ITransientService1>();
            provider.ResolveTwice<ITransientService2>();
        }

        Console.WriteLine("===== SCOPE 1 DISPOSED =====");

        using (var scope2 = host.Services.CreateScope())
        {
            Console.WriteLine("===== SCOPE 2 =====");

            var provider = scope2.ServiceProvider;

            provider.ResolveTwice<ISingletonService1>();
            provider.ResolveTwice<ISingletonService2>();

            provider.ResolveTwice<IScopedService1>();
            provider.ResolveTwice<IScopedService2>();

            provider.ResolveTwice<ITransientService1>();
            provider.ResolveTwice<ITransientService2>();
        }

        Console.WriteLine("===== SCOPE 2 DISPOSED =====");

        host.Run();
    }
}