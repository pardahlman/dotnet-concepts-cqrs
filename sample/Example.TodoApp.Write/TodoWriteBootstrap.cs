using Autofac;
using Concept.Service;
using Concept.Service.Opinionated;
using Example.TodoApp.Write.Registrations;

namespace Example.TodoApp.Write
{
  public class TodoWriteBootstrap : OpinionatedServiceBootstrap<TodoWriteService>
  {
    private readonly ServiceMetadata _metadata;

    public TodoWriteBootstrap()
    {
      _metadata = new ServiceMetadata
      {
        Name = nameof(TodoWriteService),
        Description = "Write side of Todo Service",
        Version = typeof(TodoWriteService).Assembly.ImageRuntimeVersion
      };
    }

    public override ServiceMetadata CreateMetadata() => _metadata;

    protected override void RegisterDependencies(ContainerBuilder builder)
    {
      builder.RegisterModule<TodoWriteServiceModule>();
    }
  }
}
