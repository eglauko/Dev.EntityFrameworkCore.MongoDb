using Dev.EntityFrameworkCore.MongoDb.Storage;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Remotion.Linq.Clauses;
using System.Linq.Expressions;

namespace Dev.EntityFrameworkCore.MongoDb.Query
{
    public class MongoDbEntityQueryableExpressionVisitorFactory : IEntityQueryableExpressionVisitorFactory
    {
        private readonly MongoClientSessionWrapper session;

        public MongoDbEntityQueryableExpressionVisitorFactory(MongoClientSessionWrapper session)
        {
            this.session = session;
        }

        public ExpressionVisitor Create(EntityQueryModelVisitor entityQueryModelVisitor, IQuerySource querySource)
        {
            return new MongoDbEntityQueryableExpressionVisitor(entityQueryModelVisitor, querySource, session);
        }
    }
}
