using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Concept.Cqrs.Write.Aggregate
{
	public static class AggregateReflectionHelper
	{
		private static readonly ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>> MethodCache;
		private static readonly ConcurrentDictionary<Type, IReadOnlyList<Type>> AggregateToEventTypeCache;

		static AggregateReflectionHelper()
		{
			MethodCache = new ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>>();
			AggregateToEventTypeCache = new ConcurrentDictionary<Type, IReadOnlyList<Type>>();
		}

		public static void Apply<TAggregate>(TAggregate aggregate, EventBase @event)
		{
			var eventApplyMethods = MethodCache.GetOrAdd(aggregate.GetType(), type =>
			{
				var result = new Dictionary<Type, MethodInfo>();
				var eventAppliers = Enumerable.Where<Type>(type
				    .GetInterfaces(), i => i.GetGenericTypeDefinition() == typeof(IEventApplier<>));

				foreach (var eventApplier in eventAppliers)
				{
					var eventType = eventApplier.GenericTypeArguments.FirstOrDefault();
					var applyMethod = eventApplier.GetMethods().FirstOrDefault();
					result.Add(eventType, applyMethod);
				}
				return result;
			});
			var suppliedEventType = @event.GetType();
			if (!eventApplyMethods.ContainsKey(suppliedEventType))
			{
				return;
			}
			eventApplyMethods[suppliedEventType].Invoke(aggregate, new object[]{ @event });
		}

		public static IReadOnlyList<Type> GetEventTypes(Type aggregateType)
		{
			return AggregateToEventTypeCache.GetOrAdd(aggregateType, type => Enumerable.Where<Type>(type
			    .GetInterfaces(), i => i.GetGenericTypeDefinition() == typeof(IEventApplier<>))
				.Select(i => i.GenericTypeArguments.FirstOrDefault())
				.ToList()
				.AsReadOnly());
		}
	}
}
