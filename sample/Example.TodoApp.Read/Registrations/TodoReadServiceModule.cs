using Autofac;
using RawRabbit.DependencyInjection.Autofac;

namespace Example.TodoApp.Read.Registrations
{
  public class TodoReadServiceModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<TodoReadService>()
        .AsSelf();

      builder
        .RegisterRawRabbit()
        .RegisterModule<CqrsReadModule>()
        .RegisterModule<DenormalizerModule>();
    }
  }
}
