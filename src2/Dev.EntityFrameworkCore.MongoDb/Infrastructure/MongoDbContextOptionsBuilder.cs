using MongoDB.Driver;
using System;

namespace Dev.EntityFrameworkCore.MongoDb.Infrastructure
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

        public MongoDbContextOptionsBuilder UseMongoUrl(MongoUrl url)
        {
            extension.MongoUrl = url ?? throw new ArgumentNullException(nameof(url));
            return this;
        }

        public MongoDbContextOptionsBuilder UseMongoUrlBuilder(Action<MongoUrlBuilder> buildAction)
        {
            if (buildAction == null)
                throw new ArgumentNullException(nameof(buildAction));

            var builder = new MongoUrlBuilder();
            buildAction(builder);
            extension.MongoUrl = builder.ToMongoUrl();

            return this;
        }
    }
}
