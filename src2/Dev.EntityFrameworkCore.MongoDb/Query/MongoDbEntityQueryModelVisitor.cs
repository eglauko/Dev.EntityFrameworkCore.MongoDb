using Microsoft.EntityFrameworkCore.Query;

namespace Dev.EntityFrameworkCore.MongoDb.Query
{
    public class MongoDbEntityQueryModelVisitor : EntityQueryModelVisitor
    {
        public MongoDbEntityQueryModelVisitor(
            EntityQueryModelVisitorDependencies dependencies, 
            QueryCompilationContext queryCompilationContext) : base(dependencies, queryCompilationContext)
        {
        }
    }
}
