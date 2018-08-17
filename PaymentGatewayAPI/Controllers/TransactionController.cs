using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.Enums;
using PaymentGatewayAPI.Service.Interface;
using PaymentGatewayAPI.Service.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        #region Properties

        private ITransactionService _transactionService;
        

        public TransactionController()
        {
            _transactionService = new TransactionService();

        }

        #endregion  

        #region Basic Methods

        // GET api/transaction/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(string id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(id))
                    return Json(await _transactionService.GetTransaction(id));
                else
                    return Json(new MessageModel("NOT", TipoClasseMensagemEnum.Transaction));
            }
            catch (Exception)
            {
                return Json(new MessageModel("NOT", TipoClasseMensagemEnum.Transaction));
            }
        }

        // POST api/transaction
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]TransactionModel transaction)
        {
            try
            {
                if (transaction != null)
                    return Json(await _transactionService.SetTransaction(transaction));
                else
                    return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Transaction));
            }
            catch (Exception)
            {
                return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Transaction));
            }
        }

        #endregion

        #region Custom Methods

        [HttpGet]
        public async Task<JsonResult> GetListTransaction(string storeId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(storeId))
                    return Json(await _transactionService.ListStoreTransaction(storeId));
                else
                    return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Transaction));
            }
            catch (Exception)
            {
                return Json(new MessageModel("ERR", TipoClasseMensagemEnum.Transaction));
            }
        }

        #endregion  


    }
}
