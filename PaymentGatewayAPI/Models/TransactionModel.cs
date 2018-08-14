using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models
{
    public class TransactionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionID { get; set; }
        public long TransactionCode { get; set; }

        public int AmountInCents { get; set; }
        public string CreditCardBrand { get; set; }
        public string CreditCardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        //public int SecurityCode { get; set; }
        public string HolderName { get; set; }
        public int InstallmentCount { get; set; }
        public string OrderReference { get; set; }
        public DateTime DateLog { get; set; }
        public int StatusCode { get; set; }
        public int OrderKey { get; set; }
    }
}
