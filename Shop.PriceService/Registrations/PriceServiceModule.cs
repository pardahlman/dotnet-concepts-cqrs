using Autofac;
using Concept.Service.RabbitMq.Bus;

namespace Shop.PriceService.Registrations
{
    public class PriceServiceModule : Module
    {
      protected override void Load(ContainerBuilder builder)
      {
        builder
          .RegisterModule<RawRabbitModule>();
        builder
          .RegisterType<PriceService>()
          .AsSelf()
          .SingleInstance();
      }
    }
}
