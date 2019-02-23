using Dev.EntityFrameworkCore.MongoDb.Infrastructure;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Dev.EntityFrameworkCore.MongoDb.Storage
{
    public class MongoClientWrapper
    {
        private readonly MongoDbContextOptionsExtension dbContextOptions;
        private readonly Dictionary<string, object> collections;
        private IMongoClient _mongoClient;
        private IMongoDatabase _database;

        public MongoClientWrapper(MongoDbContextOptionsExtension dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
            collections = new Dictionary<string, object>();
        }

        public IMongoClient MongoClient
        {
            get
            {
                if (_mongoClient == null)
                {
                    var url = dbContextOptions.MongoUrl;
                    _mongoClient = new MongoClient(url);
                }
                return _mongoClient;
            }
        }

        public IMongoDatabase MongoDatabase
        {
            get
            {
                if (_database == null)
                {
                    var url = dbContextOptions.MongoUrl;
                    _database = MongoClient.GetDatabase(url.DatabaseName);
                }
                return _database;
            }
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            if (!collections.TryGetValue(name, out var collection))
            {
                collection = MongoDatabase.GetCollection<TEntity>(name);
                collections.Add(name, collection);
            }
            return (IMongoCollection<TEntity>)collection;
        }
    }
}
