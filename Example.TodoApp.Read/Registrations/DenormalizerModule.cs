using Autofac;
using Concept.Cqrs.Read.Denormalize;
using Example.TodoApp.Read.EventHandlers;

namespace Example.TodoApp.Read.Registrations
{
  public class DenormalizerModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<TodoCreatedDenormalizer>()
        .As<IEventDenormalizer>();
      builder
        .RegisterType<TodoAssignedDenormalizer>()
        .As<IEventDenormalizer>();
    }
  }
}
