using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.Enums;
using PaymentGatewayAPI.Service.Interface;
using PaymentGatewayAPI.Service.Services;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(string id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                    return Json(await _storeService.GetStore(id));
                else
                    return Json(new MessageModel("NOT", TipoClasseMensagemEnum.Store));
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
                if (store != null)
                    return Json(await _storeService.SetStore(store));
                else
                    return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Store));
            }
            catch (System.Exception)
            {
                return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Store));
            }
        }

        #endregion  
    }
}