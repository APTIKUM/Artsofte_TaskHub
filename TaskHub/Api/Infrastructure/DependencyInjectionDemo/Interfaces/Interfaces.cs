namespace Api.Infrastructure.DependencyInjectionDemo.Interfaces
{
    public interface ISingletonService1 : IHasInstanceId { }
    public interface ISingletonService2 : IHasInstanceId { }

    public interface IScopedService1 : IHasInstanceId { }
    public interface IScopedService2 : IHasInstanceId { }

    public interface ITransientService1 : IHasInstanceId { }
    public interface ITransientService2 : IHasInstanceId { }
}
