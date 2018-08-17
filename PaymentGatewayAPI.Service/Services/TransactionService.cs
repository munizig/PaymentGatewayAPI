using GatewayApiClient.Utility;
using MongoDB.Bson;
using MongoDB.Driver;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.ClearSale;
using PaymentGatewayAPI.Contract.Enums;
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
        private string ClearSaleRejectErrorCode => "APA";

        public TransactionService()
        {
            Collection = MongoDBService.GetCollection<TransactionModel>("Transaction");
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

        /// <summary>
        /// Incluir nova transação no sistema. Faz também o envio para a Adquirente.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<MessageModel> SetTransaction(TransactionModel transaction)
        {
            MessageModel returnMessage = new MessageModel("ERR", Contract.Enums.TipoClasseMensagemEnum.Transaction);
            try
            {
                transaction.DateCreation = DateTime.Now;
                transaction.TransactionCode = Guid.NewGuid();
                //Incluir no BD
                await Collection.InsertOneAsync(transaction);

                //Validacoes
                if (transaction.StoreID == null)
                    return new MessageModel("ERR", TipoClasseMensagemEnum.Transaction, transaction.TransactionID.ToString());

                StoreService storeService = new StoreService();
                StoreModel storeModel = await storeService.GetStore(transaction.StoreID.ToString());

                if (storeModel != null && storeModel.AntiFraude)
                {
                    //TODO - IMPLEMENTAR VERIFICAÇÃO ANTIFRAUDE (MOCK)
                    ClearSaleService clearSaleService = new ClearSaleService();
                    ClearSaleResponseSendModel clearSaleResponseSendModel = await clearSaleService.RequestSendAsync(transaction);
                    if (clearSaleResponseSendModel != null && String.IsNullOrEmpty(clearSaleResponseSendModel.TransactionID))
                    {
                        if (clearSaleResponseSendModel.Orders[0].Status != ClearSaleRejectErrorCode)
                        {
                            transaction.Authorized = false;
                            var updateDefTransaction = Builders<TransactionModel>.Update.Set(x => x.Authorized, false);
                            UpdateResult updateResult = Collection.UpdateOne(x => x.TransactionID == transaction.TransactionID, updateDefTransaction);
                            return new MessageModel("NEG", TipoClasseMensagemEnum.Transaction, transaction.TransactionID.ToString());
                        }
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
                                if (item.ListaBandeiraCartao.Find(x => x == transaction.CreditCard.CreditCardBrandEnum) != null)
                                {
                                    HttpResponse resultTransaction;
                                    switch (item.IdAdquirente)
                                    {
                                        case AdquirenteEnum.Cielo:
                                            resultTransaction = _StoneTransactionService.CreateCreditCardTransaction(transaction);
                                            returnMessage = new MessageModel("SUC", TipoClasseMensagemEnum.Transaction, transaction.TransactionID.ToString());
                                            break;
                                        case AdquirenteEnum.Stone:
                                            resultTransaction = _CieloTransactionService.CreateCreditCardTransaction(transaction);
                                            returnMessage = new MessageModel("SUC", TipoClasseMensagemEnum.Transaction, transaction.TransactionID.ToString());
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                returnMessage = new MessageModel("ERR", TipoClasseMensagemEnum.Transaction);
            }
            catch (Exception)
            {
                throw;
            }

            return returnMessage;
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

    }
}
