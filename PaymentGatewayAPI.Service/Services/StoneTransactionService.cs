using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Service.Interface;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace PaymentGatewayAPI.Service.Services
{
    public class StoneTransactionService : IStoneTransactionService
    {

        //EndPoint da Stone
        private readonly Uri _endpoint = new Uri("https://transaction.stone.com.br");

        public StoneTransactionService()
        {

        }

        /// <summary>
        /// Cria e envia uma transação de cartão de crédito.
        /// </summary>
        /// <param name="transactionModel">Transação no modelo genérico de entrada no sistema.</param>
        public HttpResponse CreateCreditCardTransaction(TransactionModel transactionModel)
        {
            try
            {

                // Cria a transação.
                var transaction = TransformTransactionClassToStone(transactionModel);

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

                return httpResponse;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Obtém os dados de uma transação já existente.
        /// </summary>
        public void GetTransaction()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Faz a conversão da classe genérica de transação "TransactionModel" para a classe de transação da Stone
        /// </summary>
        /// <param name="transactionModel">Transação no modelo genérico de entrada no sistema.</param>
        /// <returns>Classe no modelo Stone.</returns>
        private CreditCardTransaction TransformTransactionClassToStone(TransactionModel transactionModel)
        {
            try
            {
                return new CreditCardTransaction()
                {
                    AmountInCents = transactionModel.AmountInCents,
                    CreditCard = new CreditCard()
                    {
                        CreditCardBrand = (GatewayApiClient.DataContracts.EnumTypes.CreditCardBrandEnum)transactionModel.CreditCard.CreditCardBrand.Value,
                        CreditCardNumber = transactionModel.CreditCard.CreditCardNumber, //"4111111111111111",
                        ExpMonth = transactionModel.CreditCard.ExpMonth, //10,
                        ExpYear = transactionModel.CreditCard.ExpYear, //22,
                        HolderName = transactionModel.CreditCard.HolderName,// "LUKE SKYWALKER",
                        SecurityCode = transactionModel.CreditCard.SecurityCode.ToString() // "123"
                    },
                    InstallmentCount = transactionModel.Installments
                };

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
