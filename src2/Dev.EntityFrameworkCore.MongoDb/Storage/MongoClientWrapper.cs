using Dev.EntityFrameworkCore.MongoDb.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MongoDB.Driver;

namespace Dev.EntityFrameworkCore.MongoDb.Storage
{
    public class MongoClientWrapper
    {
        private readonly MongoDbContextOptionsExtension dbContextOptions;
        private IMongoClient _mongoClient;
        private IMongoDatabase _database;

        public MongoClientWrapper(MongoDbContextOptionsExtension dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
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
    }
}
