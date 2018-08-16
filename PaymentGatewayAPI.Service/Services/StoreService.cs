using MongoDB.Driver;
using PaymentGatewayAPI.Contract;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Services
{
    /// <summary>
    /// Classe com métodos de CRUD para Store
    /// </summary>
    public class StoreService
    {
        private IMongoCollection<StoreModel> Collection { get; set; }

        public StoreService()
        {
            Collection = (IMongoCollection<StoreModel>)MongoDBService.GetCollection("Store");
        }

        /// <summary>
        /// Método para verificar se a loja cadastrada opta por anti fraude.
        /// </summary>
        /// <param name="storeID">ID da Loja</param>
        /// <returns></returns>
        public async Task<bool> GetAntiFraude(int storeID)
        {
            var item = await Collection.Find(x => x.StoreID == storeID).FirstOrDefaultAsync();
            if (item != null)
                return item.AntiFraude;

            return false;
        }

        //TODO - Criar método para incluir loja com dados básicos
        //  ID DA LOJA, NOME DA LOJA, CNPJ, ANTIFRAUDE
        

    }
}
