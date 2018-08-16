using System.Threading.Tasks;
using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface IStoneTransactionService
    {
        HttpResponse CreateCreditCardTransaction(TransactionModel newTransaction);
        void GetTransaction();
        //Task IncludeTransactionDB(TransactionModel transaction);
    }
}