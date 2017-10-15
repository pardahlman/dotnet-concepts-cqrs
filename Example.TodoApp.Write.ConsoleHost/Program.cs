using Concept.Service.ConsoleHost;

namespace Example.TodoApp.Write.ConsoleHost
{
  public class Program
  {
    public static void Main()
    {
      var bootstrapper = new TodoWriteBootstrap();
      ConsoleRunner.Start(bootstrapper);
    }
  }
}
