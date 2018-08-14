using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Services;

namespace PaymentGatewayAPI.Controllers
{
    [Route("[controller]")]
    public class TransactionController : Controller
    {

        //[HttpGet]
        //public Ihttp

        // GET gateway/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            var dbService = new MongoDBService("GatewayDB", "Transaction", "mongodb://");
            var item = await dbService.GetTransaction(null);

            return Json(item);
        }

        // POST gateway
        [HttpPost]
        public void Post([FromBody]string value)
        {
            var dbService = new MongoDBService("GatewayDB", "Transaction", "mongodb://");
            //await dbService.insert
            
        }

        //// PUT gateway/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
    }
}
