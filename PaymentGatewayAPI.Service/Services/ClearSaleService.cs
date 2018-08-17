using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.ClearSale;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Services
{
    /// <summary>
    /// Classe com métodos relacionados ao AntiFraude ClearSale
    /// </summary>
    public class ClearSaleService
    {
        static HttpClient client = new HttpClient();
        static string _endpointPrefix;

        public ClearSaleService()
        {
            _endpointPrefix = "https://integration.clearsale.com.br/api/";
        }

        public async Task<ClearSaleResponseSendModel> RequestSendAsync(TransactionModel transactionModel)
        {
            try
            {
                //Passando a classe genérica de transação para a classe do ClearSale
                ClearSaleRequestSendModel clearSaleRequestModel = TransformTransactionModelToClearSaleClass(transactionModel);

                HttpResponseMessage httpResponse = await client.PostAsJsonAsync($" {_endpointPrefix}order/send", clearSaleRequestModel);
                httpResponse.EnsureSuccessStatusCode();

                return await httpResponse.Content.ReadAsAsync<ClearSaleResponseSendModel>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ClearSaleRequestSendModel TransformTransactionModelToClearSaleClass(TransactionModel transactionModel)
        {
            try
            {

                decimal totalAmount =
                    Convert.ToDecimal(transactionModel.AmountInCents.ToString().Remove(transactionModel.AmountInCents.ToString().Length - 1, transactionModel.AmountInCents.ToString().Length - 2) + "." +
                    transactionModel.AmountInCents.ToString().Substring(transactionModel.AmountInCents.ToString().Length - 1, transactionModel.AmountInCents.ToString().Length - 2) + "M");

                    Enum.TryParse(transactionModel.CreditCard.CreditCardBrandEnum.ToString(), true, out CreditCardType cardBrand);

                ClearSaleRequestSendModel clearSaleRequestModel = new ClearSaleRequestSendModel()
                {
                    ApiKey = "apiKey",
                    LoginToken = "loginToken",
                    Orders = new List<Order> { new Order()
                    {
                        ID = transactionModel.OrderKey.ToString(),
                        Date = transactionModel.DateCreation,
                        Email = "Testing@clearsale.com.br",
                        TotalItems = 1,
                        TotalOrder = totalAmount,
                        TotalShipping = 0,
                        IP = "127.0.0.1",
                        Currency = transactionModel.Currency.ToString(),
                        Payments = new List<Payment> { new Payment() {
                            Date = DateTime.Now,
                            Type = 1, //CreditCard
                            CardNumber = transactionModel.CreditCard.CreditCardNumber,
                            CardHolderName = transactionModel.CreditCard.HolderName,
                            CardExpirationDate = $"{ transactionModel.CreditCard.ExpMonth }/{ transactionModel.CreditCard.ExpYear }",
                            Amount = totalAmount,
                            PaymentTypeID = 1,
                            CardType = (int)cardBrand,
                            CardBin = transactionModel.CreditCard.SecurityCode.ToString()
                        } }
                    } },
                    AnalysisLocation = transactionModel.CountryLocation
                };

                return clearSaleRequestModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
