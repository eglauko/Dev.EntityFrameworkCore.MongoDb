using MongoDB.Driver;
using System;

namespace Valles.EntityFrameworkCore.MongoDb.Infrastructure
{
    public class MongoDbContextOptionsBuilder
    {
        private readonly MongoDbContextOptionsExtension extension;

        public MongoDbContextOptionsBuilder(MongoDbContextOptionsExtension extension)
        {
            this.extension = extension;
        }

        public MongoDbContextOptionsBuilder UseConnectionString(string connectionString)
        {
            extension.ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            return this;
        }

        public MongoDbContextOptionsBuilder UseConnectionString(MongoUrl url)
        {
            extension.MongoUrl = url ?? throw new ArgumentNullException(nameof(url));
            return this;
        }
    }
}
