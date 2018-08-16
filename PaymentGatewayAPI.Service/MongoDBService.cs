using MongoDB.Driver;

namespace PaymentGatewayAPI.Service
{
    public static class MongoDBService
    {
        private static MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase mongoDataBase = mongoClient.GetDatabase("GatewayDB");

        ///// <summary>
        ///// Serviço para conectar ao DB de Transaction, no Mongo.
        ///// </summary>
        ///// <param name="databaseName">Database's Name. In this case: </param>
        ///// <param name="collectionName">Collection's Name to search.</param>
        ///// <param name="databaseUrl">Database's Url to connect.</param>
        public static IMongoCollection<dynamic> GetCollection(string collectionName)
        {
            IMongoCollection<dynamic> GenericCollection = mongoDataBase.GetCollection<dynamic>(collectionName);
            return GenericCollection;
        }

        //public async Task InsertTransaction(TransactionModel transaction)
        //{
        //    //=> await TransactionCollection.InsertOneAsync(transaction);
        //}

        //public async Task<List<TransactionModel>> ListTransaction()
        //{
        //    var transactions = await TransactionCollection.FindAsync(x => true);
        //    return await transactions.ToListAsync();
        //}

        //public async Task<TransactionModel> GetTransaction(Guid transactionCode)
        //{
        //    var transactions = await TransactionCollection.FindAsync(x => x.TransactionCode == transactionCode);
        //    return await transactions.FirstOrDefaultAsync();
        //}

    }
}
