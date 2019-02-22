using Dev.EntityFrameworkCore.MongoDb.Query;
using Dev.EntityFrameworkCore.MongoDb.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.EntityFrameworkCore.MongoDb.Infrastructure
{
    public class MongoDbServicesBuilder : EntityFrameworkServicesBuilder
    {
        public MongoDbServicesBuilder(IServiceCollection serviceCollection, MongoDbContextOptionsExtension extension)
            : base(serviceCollection)
        {
            TryAdd<IDatabaseProvider, DatabaseProvider<MongoDbContextOptionsExtension>>();
            TryAdd<IDatabase, MongoDatabaseWrapper>();
            TryAdd<IQueryContextFactory, MongoDbQueryContextFactory>();
            TryAdd<IEntityQueryModelVisitorFactory, MongoDbEntityQueryModelVisitorFactory>();
            TryAdd<IEntityQueryableExpressionVisitorFactory, MongoDbEntityQueryableExpressionVisitorFactory>();
            TryAdd<ITypeMappingSource, MongoDbTypeMappingSource>();

            TryAddProviderSpecificServices(
                    map => map
                        .TryAddSingleton<MongoClientWrapper, MongoClientWrapper>()
                        .TryAddScoped<MongoClientSessionWrapper, MongoClientSessionWrapper>()
                        .TryAddSingleton(extension)
                );
        }
    }
}
