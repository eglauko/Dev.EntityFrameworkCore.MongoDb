using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Dev.EntityFrameworkCore.MongoDb.Storage
{
    public class MongoClientSessionWrapper : IDisposable
    {
        private readonly MongoClientWrapper mongoClient;
        private IClientSessionHandle _clientSession;
        private Dictionary<string, IMongoCollection<BsonDocument>> collections;

        public MongoClientSessionWrapper(MongoClientWrapper mongoClient)
        {
            this.mongoClient = mongoClient;
            collections = new Dictionary<string, IMongoCollection<BsonDocument>>();
        }

        public IClientSessionHandle ClientSession
        {
            get
            {
                if (_clientSession == null)
                    _clientSession = mongoClient.MongoClient.StartSession();
                return _clientSession;
            }
        }

        public void StartTransaction(TransactionOptions options = null)
            => ClientSession.StartTransaction(options ?? new TransactionOptions(
                readConcern: ReadConcern.Snapshot,
                writeConcern: WriteConcern.WMajority));

        public void AbortTransaction() => ClientSession.AbortTransaction();

        public void CommitTransaction() => ClientSession.CommitTransaction();

        public IMongoCollection<BsonDocument> GetCollection(string name)
        {
            if (!collections.TryGetValue(name, out var collection))
            {
                collection = mongoClient.MongoDatabase.GetCollection<BsonDocument>(name);
                collections.Add(name, collection);
            }
            return collection;
        }
            

        public void Dispose()
        {
            if (_clientSession != null)
            {
                var old = _clientSession;
                _clientSession = null;
                old.Dispose();
                collections.Clear();
            }
        }
    }
}