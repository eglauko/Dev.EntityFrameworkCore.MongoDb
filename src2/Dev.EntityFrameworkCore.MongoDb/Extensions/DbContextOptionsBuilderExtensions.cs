using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Dev.EntityFrameworkCore.MongoDb.Infrastructure;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseMongoDb(
            this DbContextOptionsBuilder optionsBuilder,
            string connectionString)
        {
            return optionsBuilder.UseMongoDb(b => b.UseConnectionString(connectionString));
        }

        public static DbContextOptionsBuilder UseMongoDb(
            this DbContextOptionsBuilder optionsBuilder,
            Action<MongoDbContextOptionsBuilder> builderAction)
        {
            return SetupMongoDb(optionsBuilder, builderAction);
        }

        private static DbContextOptionsBuilder SetupMongoDb(
            DbContextOptionsBuilder optionsBuilder,
            Action<MongoDbContextOptionsBuilder> builderAction)
        {
            MongoDbContextOptionsExtension extension = new MongoDbContextOptionsExtension();

            var builder = new MongoDbContextOptionsBuilder(extension);

            builderAction(builder);

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            optionsBuilder.ConfigureWarnings(wcb => wcb.Default(WarningBehavior.Log));

            return optionsBuilder;
        }
    }
}
