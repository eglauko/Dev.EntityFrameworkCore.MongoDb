using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.EntityFrameworkCore.MongoDb.Infrastructure
{
    public class MongoDbModelCustomizer : ModelCustomizer
    {
        public MongoDbModelCustomizer(ModelCustomizerDependencies dependencies) : base(dependencies)
        {
        }

        public override void Customize(ModelBuilder modelBuilder, DbContext context)
        {
            modelBuilder.

            base.Customize(modelBuilder, context);
        }
    }
}
