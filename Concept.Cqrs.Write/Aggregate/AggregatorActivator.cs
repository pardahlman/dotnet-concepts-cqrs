using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Concept.Cqrs.Write.Aggregate
{
	public interface IAggregatorActivator
	{
		TAggregate CreateInstance<TAggregate>(List<CommittedEvent> events) where TAggregate : AggregateBase;
	}

	public class AggregatorActivator : IAggregatorActivator
	{
		private readonly ConcurrentDictionary<Type, ConstructorInfo> _typeToCtor;

		public AggregatorActivator()
		{
			_typeToCtor = new ConcurrentDictionary<Type, ConstructorInfo>();
		}

		public TAggregate CreateInstance<TAggregate>(List<CommittedEvent> events) where TAggregate : AggregateBase
		{
			var aggregateType = typeof(TAggregate);
			var constructor = _typeToCtor.GetOrAdd(aggregateType, t =>
			{
				return aggregateType
					.GetConstructors()
					.FirstOrDefault(c => c.GetParameters().Length == 0);
			});
			var aggregate = (TAggregate)constructor.Invoke(new object[0]);
			aggregate.Load(events);
			return aggregate;
		}
	}
}
