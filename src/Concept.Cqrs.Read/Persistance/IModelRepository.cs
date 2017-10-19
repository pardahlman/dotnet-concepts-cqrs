using System;
using System.Threading.Tasks;

namespace Concept.Cqrs.Read.Persistance
{
	public interface IModelRepository<TModel> : IReadOnlyRepository<TModel>, IWriteRepository<TModel> where TModel : ReadModelBase { }

	public interface IReadOnlyRepository<TModel> where TModel : ReadModelBase
	{
		Task<TModel> GetAsync(Guid aggregateId);
	}

	public interface IWriteRepository<in TModel> where TModel : ReadModelBase
	{
		Task AddOrUpdate(TModel model);
	}
}