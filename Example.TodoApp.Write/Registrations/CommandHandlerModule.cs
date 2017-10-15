using Autofac;
using Concept.Cqrs.Write;
using Example.TodoApp.Write.CommandHandlers;

namespace Example.TodoApp.Write.Registrations
{
  public class CommandHandlerModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<AssignHandler>()
        .As<ICommandHandler>();
      builder
        .RegisterType<CreateTodoHandler>()
        .As<ICommandHandler>();
      builder
        .RegisterType<ChangeStateHandler>()
        .As<ICommandHandler>();
      builder
        .RegisterType<DeleteHandler>()
        .As<ICommandHandler>();
      
      base.Load(builder);
    }
  }
}
