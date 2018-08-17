using GatewayApiClient.Utility;
using NSubstitute;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Service.Interface;
using PaymentGatewayAPI.Service.Services;
using Xunit;

namespace PaymentGatewayAPI.Tests
{
    public class Testes
    {
        private IStoneTransactionService mock;
        private readonly TransactionModel invalidCreditCard = new TransactionModel
        {
            AmountInCents = 10000,
            CreditCard = new CreditCardModel()
            {
                CreditCardBrandEnum = Contract.Enums.CreditCardBrandEnum.Elo,
                CreditCardNumber = "525241AAA",
                ExpMonth = 8,
                ExpYear = 2019,
                HolderName = "FULANO DA SILVA",
                SecurityCode = 223
            },
            Currency = Contract.Enums.CurrencyEnum.BRL,
            Installments = 1
            //TransactionCode 
            //StatusCode
        };

        public Testes()
        {
            mock = Substitute.For<IStoneTransactionService>();

            //Cartão de crédito inválido
            mock.CreateCreditCardTransaction(invalidCreditCard).Returns(new HttpResponse("", System.Net.HttpStatusCode.BadRequest));

            //mock.CreateCreditCardTransaction().Returns(new GatewayApiClient.Utility.HttpResponse("", System.Net.HttpStatusCode.BadRequest));
        }


        [Fact]
        public void TestarCartaoCreditoInvalido()
        {
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, mock.CreateCreditCardTransaction(invalidCreditCard).HttpStatusCode);

        }
    }
}
