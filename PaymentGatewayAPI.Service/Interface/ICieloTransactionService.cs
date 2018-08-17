using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface ICieloTransactionService
    {
        /// <summary>
        /// Cria transação de Cartao de Credito
        /// </summary>
        /// <param name="transactionModel"></param>
        /// <returns></returns>
        HttpResponse CreateCreditCardTransaction(TransactionModel transactionModel);
    }
}