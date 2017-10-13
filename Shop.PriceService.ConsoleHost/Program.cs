using Concept.Service.ConsoleHost;

namespace Shop.PriceService.ConsoleHost
{
  public class Program
  {
    static void Main()
    {
      var serviceConfigurer = new PriceServiceBootstrap();
      ConsoleRunner.Start(serviceConfigurer);
    }
  }
}
