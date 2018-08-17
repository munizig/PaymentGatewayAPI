using CieloEcommerce;
using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Service.Interface;
using System;

namespace PaymentGatewayAPI.Service.Services
{
    public class CieloTransactionService : ICieloTransactionService
    {
        public HttpResponse CreateCreditCardTransaction(TransactionModel transactionModel)
        {
            try
            {
                //...
                String mid = "1006993069";
                String key = "25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3";

                Cielo cielo = new Cielo(mid, key, Cielo.TEST);

                Holder holder = cielo.holder("4012001038443335", "2018", "05", "123");
                holder.name = "Fulano Portador da Silva";

                Random randomOrder = new Random();

                CieloEcommerce.Order order = cielo.order(randomOrder.Next(1000, 10000).ToString(), 10000);
                PaymentMethod paymentMethod = cielo.paymentMethod(PaymentMethod.VISA, PaymentMethod.CREDITO_A_VISTA);

                Transaction transaction = cielo.transactionRequest(
                    holder,
                    order,
                    paymentMethod,
                    "http://localhost/cielo",
                    Transaction.AuthorizationMethod.AUTHORIZE_WITHOUT_AUTHENTICATION,
                    false
                );

                if (transaction != null && transaction.order != null && string.IsNullOrWhiteSpace(transaction.order.number))
                    return new HttpResponse(transaction.status.ToString(), System.Net.HttpStatusCode.OK);

                return new HttpResponse("Erro ao efetuar a transação Cielo.", System.Net.HttpStatusCode.InternalServerError);

            }
            catch (Exception)
            {
                throw;
            }
            //// ...
            //// Configure seu merchant
            //Merchant merchant = new Merchant("MERCHANT ID", "MERCHANT KEY");

            //// Crie uma instância de Sale informando o ID do pagamento
            //Sale sale = new Sale("ID do pagamento");

            //// Crie uma instância de Customer informando o nome do cliente
            //Customer customer = sale.customer("Comprador Teste");

            //// Crie uma instância de Payment informando o valor do pagamento
            //Payment payment = sale.payment(15700);

            //// Crie  uma instância de Credit Card utilizando os dados de teste
            //// esses dados estão disponíveis no manual de integração
            //payment.creditCard("123", "Visa").setExpirationDate("12/2018")
            //                                 .setCardNumber("0000000000000001")
            //                                 .setHolder("Fulano de Tal");

            //// Crie o pagamento na Cielo
            //try
            //{
            //    // Configure o SDK com seu merchant e o ambiente apropriado para criar a venda
            //    sale = new CieloEcommerce(merchant, Environment.SANDBOX).createSale(sale);

            //    // Com a venda criada na Cielo, já temos o ID do pagamento, TID e demais
            //    // dados retornados pela Cielo
            //    String paymentId = sale.getPayment().getPaymentId();

            //    // Com o ID do pagamento, podemos fazer sua captura, se ela não tiver sido capturada ainda
            //    sale = new CieloEcommerce(merchant, Environment.SANDBOX).captureSale(paymentId, 15700, 0);

            //    // E também podemos fazer seu cancelamento, se for o caso
            //    sale = new CieloEcommerce(merchant, Environment.SANDBOX).cancelSale(paymentId, 15700);
            //}
            //catch (CieloRequestException e)
            //{
            //    // Em caso de erros de integração, podemos tratar o erro aqui.
            //    // os códigos de erro estão todos disponíveis no manual de integração.
            //    CieloError error = e.getError();
            //}
            //catch (IOException e)
            //{
            //    e.printStackTrace();
            //}
            //// ..
        }

        /// <summary>
        /// Faz a conversão da classe genérica de transação "TransactionModel" para a classe de transação da Stone
        /// </summary>
        /// <param name="transactionModel">Transação no modelo genérico de entrada no sistema.</param>
        /// <returns>Classe no modelo Stone.</returns>
        private Transaction TransformTransactionClassToStone(TransactionModel transactionModel)
        {
            try
            {
                PaymentMethod creditCardBrand = null;
                switch (transactionModel.CreditCard.CreditCardBrandEnum)
                {
                    case Contract.Enums.CreditCardBrandEnum.Visa:
                        creditCardBrand = new PaymentMethod(PaymentMethod.VISA);
                        break;
                    case Contract.Enums.CreditCardBrandEnum.Amex:
                        creditCardBrand = new PaymentMethod(PaymentMethod.AMEX);
                        break;
                    case Contract.Enums.CreditCardBrandEnum.Mastercard:
                        creditCardBrand = new PaymentMethod(PaymentMethod.MASTERCARD);
                        break;
                    case Contract.Enums.CreditCardBrandEnum.Aura:
                        creditCardBrand = new PaymentMethod(PaymentMethod.AURA);
                        break;
                    case Contract.Enums.CreditCardBrandEnum.Diners:
                        creditCardBrand = new PaymentMethod(PaymentMethod.DINERS);
                        break;
                    case Contract.Enums.CreditCardBrandEnum.Elo:
                        creditCardBrand = new PaymentMethod(PaymentMethod.ELO);
                        break;
                }

                if (creditCardBrand == null)
                    throw new Exception($"Bandeira do cartão não encontrada. Origem: { transactionModel.CreditCard.CreditCardBrandEnum.ToString() }");


                return new Transaction()
                {
                    holder = new Holder(transactionModel.CreditCard.CreditCardNumber, transactionModel.CreditCard.ExpYear.ToString(),
                                        transactionModel.CreditCard.ExpMonth.ToString(), transactionModel.CreditCard.SecurityCode.ToString())
                    { name = transactionModel.CreditCard.HolderName },
                    order = new CieloEcommerce.Order(transactionModel.TransactionID.ToString(), transactionModel.AmountInCents),
                    paymentMethod = new PaymentMethod(creditCardBrand.issuer, (transactionModel.Installments > 1 ? PaymentMethod.PARCELADO_ADM : PaymentMethod.CREDITO_A_VISTA), transactionModel.Installments)
                };

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
