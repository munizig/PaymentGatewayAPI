using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Services;

namespace PaymentGatewayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {

        MongoDBService mongoDBService;

        public TransactionController()
        {
            mongoDBService = new MongoDBService("Transaction");
        }

        // GET api/transaction
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var lista = await mongoDBService.ListTransaction();
            return Json(lista);
        }

        // GET api/transaction/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(long id)
        {
            var item = await mongoDBService.GetTransaction(id);
            return Json(item);
        }

        // POST api/transaction
        [HttpPost]
        public async Task Post([FromBody]TransactionModel transaction)
        {
            transaction.DateLog = DateTime.Now;
            transaction.TransactionCode = new Random(DateTime.Now.Millisecond).Next(500, int.MaxValue);
            await mongoDBService.InsertTransaction(transaction);
        }

        //// PUT api/transaction/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
    }
}
