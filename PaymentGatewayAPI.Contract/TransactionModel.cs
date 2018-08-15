using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PaymentGatewayAPI.Contract
{
    public class TransactionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionID { get; set; }
        public long TransactionCode { get; set; }

        
        public string OrderReference { get; set; }
        public DateTime DateLog { get; set; }
        public int StatusCode { get; set; }
        public int OrderKey { get; set; }
    }
}
