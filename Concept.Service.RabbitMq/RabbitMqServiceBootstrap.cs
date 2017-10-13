using Autofac;
using Concept.Service.Opinionated;
using RawRabbit.Instantiation;

namespace Concept.Service.RabbitMq
{
  public abstract class RabbitMqServiceBootstrap<TService> : OpinionatedServiceBootstrap<TService> where TService : Service
  {
    public override void Dispose()
    {
      var instanceFactory = AutofacContainer.Resolve<IInstanceFactory>() as InstanceFactory;
      instanceFactory?
        .ShutdownAsync()
        .ConfigureAwait(false)
        .GetAwaiter()
        .GetResult();
      base.Dispose();
    }
  }
}
