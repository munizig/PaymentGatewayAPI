using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface ICieloTransactionService
    {
        HttpResponse CreateCreditCardTransaction(TransactionModel transactionModel);
    }
}