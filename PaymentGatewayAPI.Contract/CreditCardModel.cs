﻿using PaymentGatewayAPI.Contract.Enums;
using System;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract
{
    [DataContract]
    public class CreditCardModel
    {
        #region CreditCardBrand

        [DataMember]
        public string CreditCardNumber { get; set; }

        [DataMember]
        public int ExpMonth { get; set; }

        [DataMember]
        public int ExpYear { get; set; }

        [DataMember]
        public int SecurityCode { get; set; }

        [DataMember]
        public string HolderName { get; set; }

        /// <summary>
        /// Bandeira do cartão de crédito
        /// </summary>
        [DataMember(Name = "CreditCardBrand", EmitDefaultValue = false)]
        private string CreditCardBrandField
        {
            get
            {
                if (this.CreditCardBrand == null) { return null; }
                return this.CreditCardBrand.Value.ToString();
            }
            set
            {
                if (value == null)
                    this.CreditCardBrand = null;
                else
                    this.CreditCardBrand = (CreditCardBrandEnum)Enum.Parse(typeof(CreditCardBrandEnum), value);
            }
        }

        /// <summary>
        /// Bandeira do cartão de crédito
        /// </summary>
        [IgnoreDataMember]
        public CreditCardBrandEnum? CreditCardBrand { get; set; }

        #endregion
    }
}
