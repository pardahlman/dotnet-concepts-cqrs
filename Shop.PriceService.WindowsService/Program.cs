using Concept.Service.WindowsService;

namespace Shop.PriceService.WindowsService
{
  public class Program
  {
    public static void Main()
    {
      var serviceConfigurer = new PriceServiceBootstrap();
      TopShelfRunner.Start(serviceConfigurer);
    }
  }
}
