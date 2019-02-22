﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Text;

namespace Valles.EntityFrameworkCore.MongoDb.Infrastructure
{
    public class MongoDbContextOptionsExtension : IDbContextOptionsExtension
    {

        private string _logFragment;

        /// <summary>
        /// Connection string to build <see cref="MongoUrl"/>.
        /// </summary>
        public virtual string ConnectionString
        {
            get => MongoUrl?.ToString();
            set => MongoUrl = MongoUrl.Create(value);
        }

        /// <summary>
        /// <para>
        ///     MongoDb componente used to create connections to the server.
        /// </para>
        /// <para>
        ///     Represents an immutable URL style connection string. See also <see cref="MongoUrlBuilder"/>.
        /// </para>
        /// </summary>
        public virtual MongoUrl MongoUrl { get; set; }

        #region IDbContextOptionsExtension

        public string LogFragment
        {
            get
            {
                if (_logFragment == null)
                {
                    var logBuilder = new StringBuilder();

                    if (MongoUrl != null)
                    {
                        logBuilder.Append("MongoUrl=").Append(MongoUrl.ToString());
                    }
                    
                    _logFragment = logBuilder.ToString();
                }
                return _logFragment;
            }
        }

        public bool ApplyServices(IServiceCollection services)
        {
            var servicesBuilder = new MongoDbServicesBuilder(services);
            servicesBuilder.TryAddCoreServices();
            return true;
        }

        public long GetServiceProviderHashCode() => 51_481_626_487_740_572;

        public void PopulateDebugInfo(IDictionary<string, string> debugInfo) { }

        public void Validate(IDbContextOptions options) { }

        #endregion
    }
}
