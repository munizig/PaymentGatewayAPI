using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.Enums;
using PaymentGatewayAPI.Service.Interface;
using PaymentGatewayAPI.Service.Services;
using System;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    /// <summary>
    /// Controller para Leitura e Inclusão de Transações via Gateway
    /// </summary>
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
        /// <summary>
        /// Método Get para uma Transação
        /// </summary>
        /// <param name="id">Guid da Transação</param>
        /// <returns>Objeto Json contendo a transação. Em caso de erro retorna Json com mensagem de status.</returns>
        /// <remarks>
        /// Sample Value:
        /// 
        ///     "a33e5a7a-53f7-104c-a66c-e9cbada421ab"
        /// 
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                    return Json(await _transactionService.GetTransaction(id.ToString()));
                else
                    return Json(new MessageModel("NOT", TipoClasseMensagemEnum.Transaction));
            }
            catch (Exception)
            {
                return Json(new MessageModel("NOT", TipoClasseMensagemEnum.Transaction));
            }
        }

        /// <summary>
        /// Método que faz a inclusão de um fluxo de Transação.
        /// </summary>
        /// <param name="transaction">Transaction à ser executada.</param>
        /// <returns>Objeto Json no formato MessageModel, contendo a mensagens de status.</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST
        ///		{
        ///			   "AmountInCents" : 105150,
        ///			   "Currency" : "BRL",
        ///			   "Installments" : 1,
        ///			   "CreditCard" : {
        ///			   	"CreditCardBrand" : 1, 
        ///			   	"CreditCardNumber" : "5522666587598401",
        ///			   	"ExpMonth" : 8,
        ///			   	"ExpYear" : 19,
        ///			   	"HolderName" : "IGOR MUNIZ",
        ///			   	"SecurityCode" : 558
        ///			   },
        ///			   "StoreID" : "a33e5a7a-53f7-104c-a66c-e9cbada421ab",
        ///			   "TotalItems" : 1,
        ///			   "CountryLocation" : "BRA"
        ///		}
        /// 
        /// </remarks>
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

        /// <summary>
        /// Retorna uma lista de Transações por Loja/Cliente.
        /// </summary>
        /// <param name="storeId">Guid da Store.</param>
        /// <returns>Objeto Json contendo as transações já registradas. Em caso de erro retorna Json com mensagem de status.</returns>
        /// <remarks>
        /// Sample Value:
        /// 
        ///     "a33e5a7a-53f7-104c-a66c-e9cbada421ab"
        /// 
        /// </remarks>
        [HttpGet]
        public async Task<JsonResult> GetListTransaction(Guid storeId)
        {
            try
            {
                if (storeId != Guid.Empty)
                    return Json(await _transactionService.ListStoreTransaction(storeId.ToString()));
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
