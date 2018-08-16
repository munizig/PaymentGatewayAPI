using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentGatewayAPI.Contract;
using PaymentGatewayAPI.Contract.Enums;
using System;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract
{
    public class TransactionModel
    {
        public TransactionModel()
        {
            //DateLog = DateTime.Now;
            TransactionCode = Guid.NewGuid();
        }

        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionID { get; set; }
        [DataMember]
        public Guid TransactionCode { get; set; }

        [DataMember]
        public int AmountInCents { get; set; }

        #region Currency

        [DataMember(Name = "Currency", EmitDefaultValue = false)]
        private string CurrencyField
        {
            get
            {
                if (this.Currency == null) { return null; }
                return this.Currency.Value.ToString();
            }
            set
            {
                if (value == null)
                    this.Currency = null;
                else
                    this.Currency = (CurrencyEnum)Enum.Parse(typeof(CurrencyEnum), value);
            }
        }

        /// <summary>
        /// Bandeira do cartão de crédito
        /// </summary>
        [IgnoreDataMember]
        public CurrencyEnum? Currency { get; set; }

        #endregion

        [DataMember]
        public CreditCardModel CreditCard { get; set; }

        [DataMember]
        public int Installments { get; set; }
        [DataMember]
        public string OrderReference { get; set; }
        [DataMember]
        public DateTime DateCreation { get; set; }
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember]
        public int OrderKey { get; set; }

        
        /// <summary>
        /// Código da LOJA solicitante da transação
        /// </summary>
        [DataMember]
        public int StoreID { get; set; }
    }
}
