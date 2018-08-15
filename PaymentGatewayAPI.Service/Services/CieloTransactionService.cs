using CieloEcommerce;
using System;

namespace PaymentGatewayAPI.Service.Services
{
    public class CieloTransactionService
    {
        public void CreateCreditCardTransaction()
        {
            //...
            String mid = "1006993069";
            String key = "25fbb99741c739dd84d7b06ec78c9bac718838630f30b112d033ce2e621b34f3";

            Cielo cielo = new Cielo(mid, key, Cielo.TEST);

            Holder holder = cielo.holder("4012001038443335", "2018", "05", "123");
            holder.name = "Fulano Portador da Silva";

            Random randomOrder = new Random();

            Order order = cielo.order(randomOrder.Next(1000, 10000).ToString(), 10000);
            PaymentMethod paymentMethod = cielo.paymentMethod(PaymentMethod.VISA, PaymentMethod.CREDITO_A_VISTA);

            Transaction transaction = cielo.transactionRequest(
                holder,
                order,
                paymentMethod,
                "http://localhost/cielo",
                Transaction.AuthorizationMethod.AUTHORIZE_WITHOUT_AUTHENTICATION,
                false
            );

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
    }
}
