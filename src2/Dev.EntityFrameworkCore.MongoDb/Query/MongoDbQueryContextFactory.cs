using Dev.EntityFrameworkCore.MongoDb.Storage;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Dev.EntityFrameworkCore.MongoDb.Query
{
    public class MongoDbQueryContextFactory : IQueryContextFactory
    {
        private readonly QueryContextDependencies dependencies;
        private readonly MongoClientSessionWrapper session;

        public MongoDbQueryContextFactory(
            QueryContextDependencies dependencies,
            MongoClientSessionWrapper session)
        {
            this.dependencies = dependencies;
            this.session = session;
        }

        public QueryContext Create() => new MongoDbQueryContext(dependencies, CreateQueryBuffer, session);
        

        /// <summary>
        ///     Creates a query buffer.
        /// </summary>
        /// <returns>
        ///     The new query buffer.
        /// </returns>
        private IQueryBuffer CreateQueryBuffer() => new QueryBuffer(dependencies);
    }
}
