using MongoDB.Bson;
using PaymentGatewayAPI.Contract;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface IStoreService
    {
        /// <summary>
        /// Método para obter os dados de uma Store
        /// </summary>
        /// <param name="storeID">ObjectID da Loja</param>
        /// <returns>StoreModel</returns>
        Task<StoreModel> GetStore(string storeID);

        /// <summary>
        /// Método para criar uma nova Store
        /// </summary>
        /// <param name="storeModel">Objeto Store para incluir.</param>
        /// <returns>ObjectId da Store incluída.</returns>
        Task<MessageModel> SetStore(StoreModel storeModel);
    }
}