using Autofac;
using Concept.Service.RabbitMq.Bus;

namespace Example.TodoApp.Write.Registrations
{
  public class TodoWriteServiceModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<TodoWriteService>()
        .AsSelf();

      builder
        .RegisterModule<RawRabbitModule>()
        .RegisterModule<CqrsWriteModule>()
        .RegisterModule<CommandHandlerModule>();
    }
  }
}
