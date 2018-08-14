using MongoDB.Bson;
using MongoDB.Driver;
using PaymentGatewayAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Services
{
    public class MongoDBService
    {
        private IMongoCollection<TransactionModel> TransactionCollection { get; }
        /// <summary>
        /// Serviço para conectar ao DB de Transaction, no Mongo.
        /// </summary>
        /// <param name="databaseName">Database's Name. In this case: </param>
        /// <param name="collectionName">Collection's Name to search.</param>
        /// <param name="databaseUrl">Database's Url to connect.</param>
        public MongoDBService(string databaseName, string collectionName, string databaseUrl)
        {

            var mongoClient = new MongoClient(databaseUrl);
            var mongoDataBase = mongoClient.GetDatabase(databaseName);

            TransactionCollection = mongoDataBase.GetCollection<TransactionModel>(collectionName);
        }

        public async Task InsertTransaction(TransactionModel transaction) => await TransactionCollection.InsertOneAsync(transaction);

        public async Task<TransactionModel> GetTransaction(ObjectId? transactionId = null)
        {
            //var transaction = new TransactionModel();
            var transactions = await TransactionCollection.FindAsync(new BsonDocument("TransactionID", transactionId));
            return await transactions.FirstOrDefaultAsync();
        }
    }
}
