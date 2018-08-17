using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract.ClearSale
{
    /// <summary>
    /// Classe para o JSON de Request do ClearSale
    /// </summary>
    [DataContract]
    public class ClearSaleRequestSendModel
    {
        [DataMember]
        public string ApiKey { get; set; }
        [DataMember]
        public string LoginToken { get; set; }
        [DataMember]
        public List<Order> Orders { get; set; }
        [DataMember]
        public string AnalysisLocation { get; set; }
        [DataMember]
        public bool Reanalysis { get { return false; } }
    }

    /// <summary>
    /// Classe Order para o JSON de Request do ClearSale
    /// </summary>
    [DataContract]
    public class Order
    {
        /// <summary>
        /// Order Identification Code
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// Order Date
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }

        /// <summary>
        /// Order e-Mail
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// Order total Value
        /// </summary>
        [DataMember]
        public Decimal TotalOrder { get; set; }

        /// <summary>
        /// Order originating IP address
        /// </summary>
        [DataMember]
        public string IP { get; set; }

        /// <summary>
        /// Order currency
        /// </summary>
        [DataMember]
        public string Currency { get; set; }

        /// <summary>
        /// Order Status - (if not set, default is New)
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// Payment Data
        /// </summary>
        [DataMember]
        public List<Payment> Payments { get; set; }

        /// <summary>
        /// Sum of Items Values
        /// </summary>
        [DataMember]
        public int TotalItems { get; set; }

        /// <summary>
        /// Shipping Value
        /// </summary>
        [DataMember]
        public int TotalShipping { get; set; }

    }

    /// <summary>
    /// Classe Payment para o JSON de Request do ClearSale
    /// </summary>
    [DataContract]
    public class Payment
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public string CardNumber { get; set; }
        [DataMember]
        public string CardHolderName { get; set; }
        [DataMember]
        public string CardExpirationDate { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public int PaymentTypeID { get; set; }
        [DataMember]
        public int CardType { get; set; }
        [DataMember]
        public string CardBin { get; set; }
    }

    ///// <summary>
    ///// Classe Address para o JSON de Request do ClearSale
    ///// </summary>
    //public class Address
    //{
    //    public string Street { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string Comp { get; set; }
    //    public string ZipCode { get; set; }
    //    public string County { get; set; }
    //    public string Number { get; set; }
    //}

    ///// <summary>
    ///// Classe Phone para o JSON de Request do ClearSale
    ///// </summary>
    //public class Phone
    //{
    //    public string Type { get; set; }
    //    public string CountryCode { get; set; }
    //    public string AreaCode { get; set; }
    //    public string Number { get; set; }
    //}

    ///// <summary>
    ///// Classe BillingData para o JSON de Request do ClearSale
    ///// </summary>
    //public class BillingData
    //{
    //    public string ID { get; set; }
    //    public string Type { get; set; }
    //    public string BirthDate { get; set; }
    //    public string Gender { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public Address Address { get; set; }
    //    public List<Phone> Phones { get; set; }
    //}

    ///// <summary>
    ///// Classe ShippingData para o JSON de Request do ClearSale
    ///// </summary>
    //public class ShippingData
    //{
    //    public string ID { get; set; }
    //    public string Type { get; set; }
    //    public string Name { get; set; }
    //    public string Gender { get; set; }
    //    public string Email { get; set; }
    //    public string BirthDate { get; set; }
    //    public Address Address { get; set; }
    //    public List<Phone> Phones { get; set; }
    //}

    /// <summary>
    /// Enum com os tipos de Cartão de Crédito para o JSON de Request do ClearSale
    /// </summary>
    [DataContract]
    public enum CreditCardType
    {
        Diners = 1,
        Mastercard = 2,
        Visa = 3,
        Others = 4,
        Amex = 5,
        Hipercard = 6,
        Aura = 7
    }
}
