using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Service.Interface;
using System;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IStoreService _storeService;

        public TransactionController()
        {

        }

        #region Basic Calls

        //// GET api/transaction
        //[HttpGet]
        //public async Task<JsonResult> Get()
        //{
        //    return Json("values1");
        //}

        // GET api/transaction/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(Guid transactionCode)
        {
            return Json(await _transactionService.GetTransaction(transactionCode));
        }

        // POST api/transaction
        [HttpPost]
        public async Task Post([FromBody]TransactionModel transaction)
        {

            bool result = await _transactionService.SetTransaction(transaction);
        }

        #endregion

        #region Custom Calls

        [HttpGet]
        public async Task<JsonResult> GetListTransaction(int storeId)
        {
            return Json(await _transactionService.ListStoreTransaction(storeId));
        }

        #endregion  


    }
}
