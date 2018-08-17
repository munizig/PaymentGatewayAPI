using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract.Enums
{
    /// <summary>
    /// Enum para selecionar qual a listagem de mensagens que será considera no MessageModel
    /// </summary>
    [DataContract]
    public enum TipoClasseMensagemEnum
    {
        [DataMember]
        Transaction = 1,

        [DataMember]
        Store = 2
    }
}
