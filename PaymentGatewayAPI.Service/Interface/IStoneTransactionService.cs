using System.Threading.Tasks;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface IStoneTransactionService
    {
        void CreateCreditCardTransaction(TransactionModel newTransaction);
        void GetTransaction();
        Task IncludeTransactionDB(TransactionModel transaction);
    }
}