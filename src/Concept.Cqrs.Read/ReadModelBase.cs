using System;

namespace Concept.Cqrs.Read
{
	public abstract class ReadModelBase
	{
		public Guid AggregateId { get; set; }
	}
}