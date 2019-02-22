using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.EntityFrameworkCore.MongoDb.Query
{
    public class MongoDbEntityQueryModelVisitorFactory : EntityQueryModelVisitorFactory
    {
        public MongoDbEntityQueryModelVisitorFactory(
            EntityQueryModelVisitorDependencies dependencies) : base(dependencies)
        {
        }

        public override EntityQueryModelVisitor Create(
            QueryCompilationContext queryCompilationContext,
            EntityQueryModelVisitor parentEntityQueryModelVisitor)
        {
            return new MongoDbEntityQueryModelVisitor(Dependencies, queryCompilationContext);
        }
    }
}
