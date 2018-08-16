using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface IStoreService
    {
        Task<bool> GetAntiFraude(int storeID);
    }
}