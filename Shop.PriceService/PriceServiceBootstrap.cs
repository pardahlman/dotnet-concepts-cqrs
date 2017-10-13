using Autofac;
using Concept.Service;
using Concept.Service.RabbitMq;
using Shop.PriceService.Registrations;

namespace Shop.PriceService
{
  public class PriceServiceBootstrap : RabbitMqServiceBootstrap<PriceService>
  {
    private readonly ServiceMetadata _metadata;

    public PriceServiceBootstrap()
    {
      _metadata = new ServiceMetadata
      {
        Name = "shop.pricing.service",
        Description = "Calculate product prices",
        Version = "0.1",
      };
    }

    public override ServiceMetadata CreateMetadata()
    {
      return _metadata;
    }

    protected override void RegisterDependencies(ContainerBuilder builder)
    {
      builder.RegisterModule<PriceServiceModule>();
    }
  }
}
