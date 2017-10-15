using Autofac;
using Concept.Cqrs.Read.MongoDb;
using Concept.Cqrs.Read.Persistance;
using Concept.Cqrs.Read.Process;
using Example.TodoApp.Read.Models;
using MongoDB.Driver;

namespace Example.TodoApp.Read.Registrations
{
  public class CqrsReadModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder
        .RegisterType<EventsProcessor>()
        .AsImplementedInterfaces();
      builder
        .RegisterType<MongoDbModelRepository<Todo>>()
        .AsImplementedInterfaces();
      builder
        .RegisterType<MongoClient>()
        .AsImplementedInterfaces();
    }
  }
}
