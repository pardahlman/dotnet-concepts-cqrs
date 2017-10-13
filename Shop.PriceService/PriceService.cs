using System;
using System.Threading.Tasks;
using Concept.Service.RabbitMq;
using Concept.Service.RabbitMq.Bus;
using RawRabbit;
using Serilog;
using Shop.Messages.V1;

namespace Shop.PriceService
{
  public class PriceService : RabbitMqService
  {
    private readonly ILogger _logger = Log.ForContext<PriceService>();

    public PriceService(IBusClient busClient)
      : base(busClient) { }

    public override async Task StartAsync()
    {
      _logger.Information("Starting service at {startTime}", DateTime.Now);
      await SubscribeAsync<CalculatePrice>(OnCalculatePrice);
    }

    private Task OnCalculatePrice(CalculatePrice message, ConceptContext context)
    {
      _logger.Information("Recieved price calculation request for product ids {@ProductIds}", message.ProductIds);
      return Task.CompletedTask;
    }
  }
}
