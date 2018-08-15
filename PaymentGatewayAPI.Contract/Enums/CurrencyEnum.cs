using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PaymentGatewayAPI.Contract.Enums
{
    /// <summary>
    /// Enumerado com as Moedas válidas para nova Transaction
    /// </summary>
    public enum CurrencyEnum
    {
        /// <summary>
        /// Real
        /// </summary>
        [EnumMember]
        BRL = 1,

        /// <summary>
        /// Dólar
        /// </summary>
        [EnumMember]
        USD = 2,

        /// <summary>
        /// Euro
        /// </summary>
        [EnumMember]
        EUR = 3
    }
}
