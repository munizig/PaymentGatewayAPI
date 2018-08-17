using PaymentGatewayAPI.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface ITransactionService
    {
        /// <summary>
        /// Obter transação por Código de Transação
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        Task<TransactionModel> GetTransaction(Guid transactionCode);

        /// <summary>
        /// Incluir nova transação no sistema. Faz também o envio para a Adquirente.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<MessageModel> SetTransaction(TransactionModel transaction);
        
        /// <summary>
        /// listar todas as transações por ID DA LOJA (StoreID)
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        Task<List<TransactionModel>> ListStoreTransaction(int storeID);
    }
}