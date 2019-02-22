using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Valles.EntityFrameworkCore.MongoDb.Infrastructure
{
    public class MongoDbServicesBuilder : EntityFrameworkServicesBuilder
    {
        public MongoDbServicesBuilder(IServiceCollection serviceCollection) : base(serviceCollection) { }
    }
}
