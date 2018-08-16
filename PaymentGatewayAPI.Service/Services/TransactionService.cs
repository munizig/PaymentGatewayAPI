using GatewayApiClient.Utility;
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
        private IStoneTransactionService _StoneTransactionService { get; set; }
        private ICieloTransactionService _CieloTransactionService { get; set; }

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
                IMongoCollection<StoreModel> storeCollection = (IMongoCollection<StoreModel>)MongoDBService.GetCollection("Store");
                var storeModel = await storeCollection.Find(x => x.StoreID == transaction.StoreID).FirstOrDefaultAsync();

                if (storeModel.AntiFraude)
                {
                    var result = 0;

                    //TODO - IMPLEMENTAR VERIFICAÇÃO ANTIFRAUDE (MOCK)
                }

                //TODO - Verificar qual adquirente o cliente tem
                if (storeModel.ListaAdquirente != null && storeModel.ListaAdquirente.Count > 0)
                {
                    foreach (var item in storeModel.ListaAdquirente)
                    {
                        //Conferir as bandeiras cadastradas para a loja
                        if (item.ListaBandeiraCartao != null && item.ListaBandeiraCartao.Count > 0)
                        {
                            //Se encontrar bandeira na adquirente igual a bandeira da transação, incluir transação por essa adquirente
                            if (item.ListaBandeiraCartao.Find(x => x == transaction.CreditCard.CreditCardBrand) != null)
                            {
                                HttpResponse resultTransaction;
                                switch (item.IdAdquirente)
                                {
                                    case AdquirenteEnum.Cielo:
                                        resultTransaction = _StoneTransactionService.CreateCreditCardTransaction(transaction);
                                        break;
                                    case AdquirenteEnum.Stone:
                                        resultTransaction = _CieloTransactionService.CreateCreditCardTransaction(transaction);
                                        break;
                                }
                            }
                        }
                    }
                }

                

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
