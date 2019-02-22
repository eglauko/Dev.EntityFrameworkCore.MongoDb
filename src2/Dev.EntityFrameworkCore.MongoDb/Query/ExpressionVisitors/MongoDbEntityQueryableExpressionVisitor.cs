using Dev.EntityFrameworkCore.MongoDb.Storage;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using MongoDB.Driver;
using Remotion.Linq.Clauses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dev.EntityFrameworkCore.MongoDb.Query
{
    public class MongoDbEntityQueryableExpressionVisitor : EntityQueryableExpressionVisitor
    {
        private readonly IQuerySource querySource;
        private readonly MongoClientSessionWrapper session;

        public MongoDbEntityQueryableExpressionVisitor(
            EntityQueryModelVisitor entityQueryModelVisitor,
            IQuerySource querySource, MongoClientSessionWrapper session) : base (entityQueryModelVisitor)
        {
            this.querySource = querySource;
            this.session = session;
        }

        protected override Expression VisitEntityQueryable(Type elementType)
        {
            var a = QueryModelVisitor.QueryCompilationContext.Model.FindEntityType(elementType.Name);
            var b = QueryModelVisitor.QueryCompilationContext.Model.FindEntityType(elementType.FullName);

            var name = (a ?? b).Name;
            var col = session.GetCollection(name);

            var query = col.AsQueryable();

            return Expression.Constant(query);
        }
    }
}
