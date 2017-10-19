using Autofac;
using Concept.Service;
using Concept.Service.Opinionated;
using Concept.Service.RabbitMq;
using Example.TodoApp.Read.Registrations;

namespace Example.TodoApp.Read
{
  public class TodoReadBootstrap : OpinionatedServiceBootstrap<TodoReadService>
  {
    private readonly ServiceMetadata _meta;

    public TodoReadBootstrap()
    {
      _meta = new ServiceMetadata
      {
        Name = nameof(TodoReadService),
        Description = "Read side of Todo Service",
      };
    }

    public override ServiceMetadata CreateMetadata() => _meta;

    protected override void RegisterDependencies(ContainerBuilder builder)
    {
      builder.RegisterModule<TodoReadServiceModule>();
    }
  }
}
