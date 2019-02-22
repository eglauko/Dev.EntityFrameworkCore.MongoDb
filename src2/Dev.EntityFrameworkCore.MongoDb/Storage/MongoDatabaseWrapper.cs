using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dev.EntityFrameworkCore.MongoDb.Storage
{
    public class MongoDatabaseWrapper : Database
    {
        public MongoDatabaseWrapper(DatabaseDependencies dependencies) : base(dependencies)
        {
        }

        public override int SaveChanges(IReadOnlyList<IUpdateEntry> entries)
        {
            throw new NotImplementedException();
        }

        public override Task<int> SaveChangesAsync(IReadOnlyList<IUpdateEntry> entries, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
