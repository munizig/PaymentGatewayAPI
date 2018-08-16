using MongoDB.Bson;
using MongoDB.Driver;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Services
{
    /// <summary>
    /// Classe com métodos de CRUD de Transaction
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private IMongoCollection<TransactionModel> Collection { get; set; }

        public TransactionService()
        {
            Collection = (IMongoCollection<TransactionModel>)MongoDBService.GetCollection("Transaction");
        }

        /// <summary>
        /// Incluir nova transação no BD
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<Boolean> SetTransaction(TransactionModel transaction)
        {
            try
            {
                await Collection.InsertOneAsync(transaction);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// listar todas as transações por ID DA LOJA (StoreID)
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public async Task<List<TransactionModel>> ListStoreTransaction(int storeID)
        {
            try
            {
                return await Collection.Find<TransactionModel>(new BsonDocument("StoreID", storeID))
                                    .Sort(new BsonDocument("DateCreation", -1))
                                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter transação por Código de Transação
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        public async Task<TransactionModel> GetTransaction(Guid transactionCode)
        {
            try
            {
                return await Collection.Find(x => x.TransactionCode == transactionCode).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
