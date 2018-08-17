using System.Threading.Tasks;
using GatewayApiClient.DataContracts;
using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface IStoneTransactionService
    {
        /// <summary>
        /// Método para obter os dados de uma Store
        /// </summary>
        /// <param name="storeID">ID da Loja</param>
        /// <returns>StoreModel</returns>
        HttpResponse<CreateSaleResponse> CreateCreditCardTransaction(TransactionModel transactionModel);

        /// <summary>
        /// Método para criar uma nova Store
        /// </summary>
        /// <param name="storeModel">Objeto Store para incluir.</param>
        /// <returns>ObjectId da Store incluída.</returns>
        void GetTransaction();
    }
}