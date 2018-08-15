using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Contract;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {

        public TransactionController()
        {

        }

        // GET api/transaction
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            return Json("values1");
        }

        // GET api/transaction/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<JsonResult> Get(long id)
        {
            return Json(new string[] { "values1", "values2" });
        }

        // POST api/transaction
        [HttpPost]
        public async Task Post([FromBody]TransactionModel transaction)
        {
            await CreateTransaction();
        }

        private async Task CreateTransaction()
        {

        }


    }
}
