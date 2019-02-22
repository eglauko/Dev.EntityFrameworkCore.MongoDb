using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Dev.EntityFrameworkCore.MongoDb.Storage
{
    public class MongoDbTypeMappingSource : TypeMappingSource
    {
        private readonly ConcurrentDictionary<Type, CoreTypeMapping> _typeCache = 
            new ConcurrentDictionary<Type, CoreTypeMapping>
            {
                [typeof(string)] = new PassThruTypeMapping(typeof(string)),
                [typeof(IEnumerable<string>)] = new PassThruTypeMapping(typeof(IEnumerable<string>)),
                [typeof(ObjectId)] = new PassThruTypeMapping(typeof(ObjectId)),
                [typeof(IEnumerable<ObjectId>)] = new PassThruTypeMapping(typeof(IEnumerable<ObjectId>)),
                [typeof(byte[])] = new PassThruTypeMapping(typeof(byte[])),
                [typeof(IEnumerable<byte[]>)] = new PassThruTypeMapping(typeof(IEnumerable<byte[]>))
            };

        public MongoDbTypeMappingSource(TypeMappingSourceDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <inheritdoc />
        protected override CoreTypeMapping FindMapping(in TypeMappingInfo mappingInfo)
            => _typeCache.GetOrAdd(
                   mappingInfo.ClrType,
                   clrType =>
                   {
                       TypeInfo typeInfo = (clrType.TryGetSequenceType() ?? clrType).UnwrapNullableType().GetTypeInfo();

                       return typeInfo.IsPrimitive
                              || typeInfo.IsValueType
                           ? new PassThruTypeMapping(clrType)
                           : null;
                   });

        private class PassThruTypeMapping : CoreTypeMapping
        {
            private PassThruTypeMapping(CoreTypeMappingParameters parameters)
                : base(parameters)
            {
            }

            public PassThruTypeMapping(Type clrType)
                : base(new CoreTypeMappingParameters(clrType))
            {
            }

            public override CoreTypeMapping Clone(ValueConverter converter)
                => new PassThruTypeMapping(Parameters.WithComposedConverter(converter));
        }
    }
}
