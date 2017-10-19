using System;
using System.Threading.Tasks;
using Concept.Cqrs.Read.Persistance;
using MongoDB.Driver;

namespace Concept.Cqrs.Read.MongoDb
{
  public class MongoDbModelRepository<TModel> : IModelRepository<TModel> where TModel : ReadModelBase
  {
    private readonly IMongoCollection<TModel> _collection;
    private const string DefaultDbName = "read_models";

    public MongoDbModelRepository(IMongoClient mongoClient)
      : this(mongoClient, DefaultDbName) { }

    public MongoDbModelRepository(IMongoClient mongoClient, string databaseName)
    {
      _collection = mongoClient.GetDatabase(databaseName).GetCollection<TModel>(typeof(TModel).FullName);
    }

    public Task<TModel> GetAsync(Guid aggregateId)
    {
      return _collection
        .Find(model => model.AggregateId == aggregateId)
        .FirstOrDefaultAsync();
    }

    public Task AddOrUpdate(TModel model)
    {
      return _collection.ReplaceOneAsync(
        m => m.AggregateId == model.AggregateId,
        model,
        new UpdateOptions {IsUpsert = true}
       );
    }
  }
}
