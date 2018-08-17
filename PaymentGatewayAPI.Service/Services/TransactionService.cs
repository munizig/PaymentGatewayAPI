using GatewayApiClient.DataContracts;
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
        private IStoneTransactionService _StoneTransactionService;
        private ICieloTransactionService _CieloTransactionService;
        private string ClearSaleRejectErrorCode => "APA";

        public TransactionService()
        {
            Collection = MongoDBService.GetCollection<TransactionModel>("Transaction");
            _StoneTransactionService = new StoneTransactionService();
            _CieloTransactionService = new CieloTransactionService();
        }

        /// <summary>
        /// Obter transação por Código de Transação
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        public async Task<TransactionModel> GetTransaction(string transactionID)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(transactionID) && Guid.TryParse(transactionID, out Guid transactionGuid))
                {
                    var filter = Builders<TransactionModel>.Filter.Eq("TransactionID", transactionGuid);
                    return await Collection.Find(filter).FirstOrDefaultAsync();
                }
                else
                    return new TransactionModel();
            }
            catch (Exception ex)
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
            try
            {
                transaction.DateCreation = DateTime.Now;
                transaction.TransactionID = Guid.NewGuid();
                //Incluir no BD
                await Collection.InsertOneAsync(transaction);

                //Validacoes
                if (transaction.StoreID == null)
                    return new MessageModel("ERR", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);

                StoreService storeService = new StoreService();
                StoreModel storeModel = await storeService.GetStore(transaction.StoreID.ToString());

                //Verificar se a loja opta pelo sistema antifraude
                if (storeModel != null)
                {
                    if (storeModel.AntiFraude)
                    {
                        //IMPLEMENTAR VERIFICAÇÃO ANTIFRAUDE (MOCK)
                        ClearSaleService clearSaleService = new ClearSaleService();
                        ClearSaleResponseSendModel clearSaleResponseSendModel = await clearSaleService.RequestSendAsync(transaction);
                        if (clearSaleResponseSendModel != null && String.IsNullOrEmpty(clearSaleResponseSendModel.TransactionID))
                        {
                            if (clearSaleResponseSendModel.Orders[0].Status != ClearSaleRejectErrorCode)
                            {
                                transaction.Authorized = false;
                                var updateDefTransaction = Builders<TransactionModel>.Update.Set(x => x.Authorized, false);
                                UpdateResult updateResult = Collection.UpdateOne(x => x.TransactionID == transaction.TransactionID, updateDefTransaction);
                                return new MessageModel("NEG", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);
                            }
                        }
                    }


                    //Verificar quais adquirente a loja tem, e quais regras seguir para cada uma
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
                                    MessageModel messageModel;

                                    switch (item.IdAdquirente)
                                    {
                                        case AdquirenteEnum.Cielo:
                                            HttpResponse<CreateSaleResponse> httpResponseStone = _StoneTransactionService.CreateCreditCardTransaction(transaction);

                                            if (httpResponseStone.Response.CreditCardTransactionResultCollection != null)
                                            {
                                                if (httpResponseStone.Response.CreditCardTransactionResultCollection[0].AcquirerReturnCode == "0") //Sucesso
                                                    return new MessageModel("SUC", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);
                                                else if (httpResponseStone.Response.CreditCardTransactionResultCollection[0].AcquirerReturnCode == "81") //Timeout
                                                    return new MessageModel("TIMEOUT", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);
                                                else if (httpResponseStone.Response.CreditCardTransactionResultCollection[0].AcquirerReturnCode == "92")
                                                    return new MessageModel("OUTCRED", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);
                                                else if (httpResponseStone.Response.CreditCardTransactionResultCollection[0].AcquirerReturnCode == "1") //Não autorizado
                                                    return new MessageModel("UNAUTH", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);
                                            }
                                            break;
                                        case AdquirenteEnum.Stone:
                                            var httpResponseCielo = _CieloTransactionService.CreateCreditCardTransaction(transaction);
                                            messageModel = new MessageModel("SUC", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);
                                            break;
                                    }

                                    break;
                                }
                            }
                            else
                            {
                                return new MessageModel("NOTCARD", TipoClasseMensagemEnum.Transaction);
                            }
                        }

                    }
                    else
                    {
                        return new MessageModel("NOTADQ", TipoClasseMensagemEnum.Transaction);
                    }
                }

                return new MessageModel("ERR", TipoClasseMensagemEnum.Transaction, transaction.TransactionIDText);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// listar todas as transações por ID DA LOJA (StoreID)
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public async Task<List<TransactionModel>> ListStoreTransaction(string storeID)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(storeID) && Guid.TryParse(storeID, out Guid storeGuid))
                    return await Collection.Find<TransactionModel>(new BsonDocument("StoreID", storeGuid))
                                        .Sort(new BsonDocument("DateCreation", -1))
                                        .ToListAsync();
                else
                    return new List<TransactionModel>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
