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
            IQuerySource querySource, 
            MongoClientSessionWrapper session) : base (entityQueryModelVisitor)
        {
            this.querySource = querySource;
            this.session = session;
        }

        protected override Expression VisitEntityQueryable(Type elementType)
        {
            var et = QueryModelVisitor.QueryCompilationContext.Model.FindEntityType(elementType.FullName);

            var name = et.FindAnnotation("DbName")?.Value ?? et.Name;

            var method = typeof(IMongoCollectionExtensions).GetMethod("AsQueryable").MakeGenericMethod(et.ClrType);
            
            return Expression.Call(
                method,
                Expression.Call(
                    Expression.Constant(session),
                    "GetCollection",
                    new Type[] { et.ClrType },
                    Expression.Constant(name)),
                Expression.Constant(
                    null, 
                    typeof(AggregateOptions)));
        }
    }
}
