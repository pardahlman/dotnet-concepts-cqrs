using Autofac;
using Concept.Cqrs.Write;
using Concept.Cqrs.Write.Aggregate;
using Concept.Cqrs.Write.MongoDb;
using Concept.Cqrs.Write.Persistance;
using Concept.Cqrs.Write.RabbitMq;
using MongoDB.Driver;

namespace Example.TodoApp.Write.Registrations
{
  public class CqrsWriteModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<CommandsProcesser>()
        .AsImplementedInterfaces();

      builder
        .RegisterType<RabbitMqEventDispatcher>()
        .AsImplementedInterfaces();

      builder
        .RegisterType<AggregateRepository>()
        .AsImplementedInterfaces();

      builder
        .RegisterType<MongoDbEventStreamRepository>()
        .AsImplementedInterfaces();

      builder
        .RegisterType<MongoClient>()
        .AsImplementedInterfaces();

      builder
        .RegisterType<StreamNameResolver>()
        .AsImplementedInterfaces();

      builder
        .RegisterType<AggregatorActivator>()
        .AsImplementedInterfaces();
    }
  }
}
