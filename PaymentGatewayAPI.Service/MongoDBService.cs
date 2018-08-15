using MongoDB.Driver;
using PaymentGatewayAPI.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service
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
        public MongoDBService(string collectionName)
        {

            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDataBase = mongoClient.GetDatabase("GatewayDB");

            TransactionCollection = mongoDataBase.GetCollection<TransactionModel>(collectionName);
        }

        public async Task InsertTransaction(TransactionModel transaction) => await TransactionCollection.InsertOneAsync(transaction);

        public async Task<List<TransactionModel>> ListTransaction()
        {
            var transactions = await TransactionCollection.FindAsync(x => true);
            return await transactions.ToListAsync();
        }

        public async Task<TransactionModel> GetTransaction(long transactionCode)
        {
            var transactions = await TransactionCollection.FindAsync(x=>x.TransactionCode == transactionCode);
            return await transactions.FirstOrDefaultAsync();
        }

    }
}
