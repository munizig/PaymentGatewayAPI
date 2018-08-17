using MongoDB.Bson;
using MongoDB.Driver;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.Enums;
using PaymentGatewayAPI.Service.Interface;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Services
{
    /// <summary>
    /// Classe com métodos de CRUD para Store
    /// </summary>
    public class StoreService : IStoreService
    {
        private IMongoCollection<StoreModel> Collection { get; set; }

        public StoreService()
        {
            Collection = MongoDBService.GetCollection<StoreModel>("Store");
        }

        /// <summary>
        /// Método para obter os dados de uma Store
        /// </summary>
        /// <param name="storeID">ID da Loja</param>
        /// <returns>StoreModel</returns>
        public async Task<StoreModel> GetStore(string storeID)
        {
            try
            {
                return await Collection.Find(x => x.ID == ObjectId.Parse(storeID)).FirstOrDefaultAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para criar uma nova Store
        /// </summary>
        /// <param name="storeModel">Objeto Store para incluir.</param>
        /// <returns>ObjectId da Store incluída.</returns>
        public async Task<MessageModel> SetStore(StoreModel storeModel)
        {
            try
            {
                //Obtem o ID do item incluído
                await Collection.InsertOneAsync(storeModel);
                return new MessageModel("SUC", TipoClasseMensagemEnum.Store, storeModel.ID.ToString());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
