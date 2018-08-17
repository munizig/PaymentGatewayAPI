using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.Enums;
using PaymentGatewayAPI.Service.Interface;
using PaymentGatewayAPI.Service.Services;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Store")]
    public class StoreController : Controller
    {
        #region Properties
        private IStoreService _storeService;

        public StoreController()
        {
            _storeService = new StoreService();
        }

        #endregion

        #region Basic Methods

        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(string id)
        {
            try
            {
                return Json(await _storeService.GetStore(id));
            }
            catch (System.Exception)
            {
                return Json(new MessageModel("NOT", TipoClasseMensagemEnum.Store));
            }
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromBody]StoreModel store)
        {
            try
            {
                return Json(await _storeService.SetStore(store));
            }
            catch (System.Exception)
            {
                return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Store));
            }
        }

        #endregion  
    }
}