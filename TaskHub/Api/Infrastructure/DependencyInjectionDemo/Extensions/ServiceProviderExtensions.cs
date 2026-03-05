using Api.Infrastructure.DependencyInjectionDemo.Interfaces;

namespace Api.Infrastructure.DependencyInjectionDemo.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void ResolveTwice<TService>(this IServiceProvider serviceProvider)
            where TService : IHasInstanceId
        {
            var first = serviceProvider.GetService<TService>();
            var second = serviceProvider.GetService<TService>();

            Console.WriteLine($"Service: {typeof(TService).Name}");
            Console.WriteLine($"First Id:  {first.InstanceId}");
            Console.WriteLine($"Second Id: {second.InstanceId}");
            Console.WriteLine($"Same instance: {ReferenceEquals(first, second)}");
            Console.WriteLine(new string('-', 50));
        }
    }
}
