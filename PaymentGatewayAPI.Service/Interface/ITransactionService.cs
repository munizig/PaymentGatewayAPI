using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface ITransactionService
    {
        Task<TransactionModel> GetTransaction(Guid transactionCode);
        Task<bool> SetTransaction(TransactionModel transaction);
        Task<List<TransactionModel>> ListStoreTransaction(int storeID);
    }
}