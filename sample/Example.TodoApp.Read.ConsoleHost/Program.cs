using Concept.Service.ConsoleHost;

namespace Example.TodoApp.Read.ConsoleHost
{
  public class Program
  {
    static void Main(string[] args)
    {
      var bootstrap = new TodoReadBootstrap();
      ConsoleRunner.Start(bootstrap);
    }
  }
}
