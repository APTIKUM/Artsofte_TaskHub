using Api.Infrastructure.DependencyInjectionDemo.Interfaces;

namespace Api.Infrastructure.DependencyInjectionDemo.Services
{
    public class SingletonService1 : DisposedService, ISingletonService1 { }
    public class SingletonService2 : DisposedService, ISingletonService2 { }

    public class ScopedService1 : DisposedService, IScopedService1 { }
    public class ScopedService2 : DisposedService, IScopedService2 { }

    public class TransientService1 : DisposedService, ITransientService1 { }
    public class TransientService2 : DisposedService, ITransientService2 { }
}