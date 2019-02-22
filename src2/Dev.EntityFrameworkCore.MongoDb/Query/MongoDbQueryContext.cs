using Dev.EntityFrameworkCore.MongoDb.Storage;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;

namespace Dev.EntityFrameworkCore.MongoDb.Query
{
    public class MongoDbQueryContext : QueryContext
    {
        public MongoClientSessionWrapper Session { get; }

        public MongoDbQueryContext(
            QueryContextDependencies dependencies,
            Func<IQueryBuffer> queryBufferFactory,
            MongoClientSessionWrapper session) : base(dependencies, queryBufferFactory)
        {
            Session = session;
        }
    }
}
