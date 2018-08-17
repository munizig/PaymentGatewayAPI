using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentGatewayAPI.Contract.Enums;
using System;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract
{

    [DataContract]
    public class TransactionModel
    {
        public TransactionModel()
        {
            //DateCreation = DateTime.Now;
            //TransactionCode = Guid.NewGuid();
        }

        [DataMember]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId TransactionID { get; set; }

        [DataMember]
        public Guid TransactionCode { get; set; }

        //public string TransactionCodeText { get { return TransactionCode.ToString(); } }

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
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId StoreID { get; set; }

        #region Campos Auxiliares

        /// <summary>
        /// Quantidade total de produtos do pedido
        /// </summary>
        [DataMember]
        public int TotalItems { get; set; }

        [DataMember]
        public string CountryLocation { get; set; }

        [DataMember]
        public bool Authorized { get; set; }

        #endregion  

    }
}
