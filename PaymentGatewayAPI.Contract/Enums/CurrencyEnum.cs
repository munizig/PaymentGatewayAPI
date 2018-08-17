using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract.Enums
{
    /// <summary>
    /// Enumerado com as Moedas válidas para nova Transaction
    /// </summary>
    [DataContract]
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
