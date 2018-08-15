using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Service.Interface;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Service.Services
{
    public class StoneTransactionService : IStoneTransactionService
    {
        //Serviço MongoDB
        MongoDBService mongoDBService;

        //EndPoint da Stone
        private readonly Uri _endpoint = new Uri("https://transaction.stone.com.br");

        public StoneTransactionService()
        {
            //Instancia da coleção Transaction no MongoDB
            mongoDBService = new MongoDBService("Transaction");
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateCreditCardTransaction(TransactionModel newTransaction)
        {

            // Cria a transação.
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 10000,
                CreditCard = new CreditCard()
                {
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    CreditCardNumber = "4111111111111111",
                    ExpMonth = 10,
                    ExpYear = 22,
                    HolderName = "LUKE SKYWALKER",
                    SecurityCode = "123"
                },
                InstallmentCount = 1
            };

            // Cria requisição.
            var createSaleRequest = new CreateSaleRequest()
            {
                // Adiciona a transação na requisição.
                CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new CreditCardTransaction[] { transaction }),
                Order = new Order()
                {
                    OrderReference = "NumeroDoPedido"
                }
            };

            // Coloque a sua MerchantKey aqui.
            Guid merchantKey = Guid.Parse("f2a1f485-cfd4-49f5-8862-0ebc438ae923");

            // Cria o client que enviará a transação.
            var serviceClient = new GatewayServiceClient(merchantKey, _endpoint);

            // Autoriza a transação e recebe a resposta do gateway.
            var httpResponse = serviceClient.Sale.Create(createSaleRequest);

            Console.WriteLine("Código retorno: {0}", httpResponse.HttpStatusCode);
            Console.WriteLine("Chave do pedido: {0}", httpResponse.Response.OrderResult.OrderKey);
            if (httpResponse.Response.CreditCardTransactionResultCollection != null)
            {
                Console.WriteLine("Status transação: {0}", httpResponse.Response.CreditCardTransactionResultCollection.FirstOrDefault().CreditCardTransactionStatus);
            }


        }

        public void GetTransaction()
        {
            Guid merchantKey = Guid.Parse("F2A1F485-CFD4-49F5-8862-0EBC438AE923");
            Guid orderKey = Guid.Parse("219d7581-78e2-4aa9-b708-b7c585780bfc");

            // Cria o cliente para consultar o pedido no gateway.
            IGatewayServiceClient client = new GatewayServiceClient(merchantKey, _endpoint);

            // Consulta o pedido.
            var httpResponse = client.Sale.QueryOrder(orderKey);

            if (httpResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                foreach (var sale in httpResponse.Response.SaleDataCollection)
                {
                    Console.WriteLine("Número do pedido: {0}", sale.OrderData.OrderReference);
                }
            }
        }

        public async Task IncludeTransactionDB(TransactionModel transaction)
        {
            transaction.DateLog = DateTime.Now;
            transaction.TransactionCode = new Random(DateTime.Now.Millisecond).Next(500, int.MaxValue);
            await mongoDBService.InsertTransaction(transaction);
        }

    }
}
