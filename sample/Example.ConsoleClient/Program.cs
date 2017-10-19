using System;
using System.Threading.Tasks;
using Example.TodoApp.Domain.V1.Commands;
using Example.TodoApp.Domain.V1.Events;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace Example.ConsoleClient
{
  class Program
  {
    static void Main(string[] args)
    {
      MainAsync(args).GetAwaiter().GetResult();
    }

    public static async Task MainAsync(string[] args)
    {
      var busClient = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
      {
        Plugins = p => p.UseMessageContext(context => new ConceptContext{ GlobalExecutionId = Guid.NewGuid(), Origin = "Console Client"})
      });
      Console.WriteLine("Press [ENTER] to create a todo");
      Console.ReadKey();
      await busClient.PublishAsync(new CreateTodo
      {
        AggregateId = Guid.NewGuid(),
        CreatedAt = DateTime.Now,
        DueDate = DateTime.Now.AddDays(7),
        CreatedBy = "pardahlman",
        Topic = "Implement function"
      });
    }
  }

  public class ConceptContext
  {
    public string Origin { get; set; }
    public Guid GlobalExecutionId { get; set; }
    public string MessageId { get; set; }
  }
}
