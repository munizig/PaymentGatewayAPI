using PaymentGatewayAPI.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract
{
    /// <summary>
    /// Classe com as informações de Adquirente da loja (um ou mais)
    /// </summary>
    [DataContract]
    public class AdquirenteModel
    {
        public AdquirenteModel()
        {
        }

        [DataMember]
        public AdquirenteEnum? IdAdquirente { get; set; }

        [DataMember]
        public string NomeAdquirente
        {
            get
            {
                if (this.IdAdquirente == null) return null;
                return this.IdAdquirente.Value.ToString();
            }
            set
            {
                this.IdAdquirente = null;
                if (value != null)
                    this.IdAdquirente = (AdquirenteEnum)Enum.Parse(typeof(AdquirenteEnum), value);
            }
        }

        /// <summary>
        /// Lista de cartões aceitos por adquirente
        /// </summary>
        [DataMember]
        public List<CreditCardBrandEnum?> ListaBandeiraCartao { get; set; }
    }


    /// <summary>
    /// Enumerado com as opções de Adquirente possíveis
    /// </summary>
    [DataContract]
    public enum AdquirenteEnum
    {
        /// <summary>
        /// Integração com a Stone
        /// </summary>
        [DataMember]
        Stone = 1,

        /// <summary>
        /// Integração com a Cielo
        /// </summary>
        [DataMember]
        Cielo = 2
    }
}
