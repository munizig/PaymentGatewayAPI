using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract
{
    /// <summary>
    /// Classe com as propriedades referentes a uma loja conveniada
    /// </summary>
    [DataContract]
    public class StoreModel
    {

        /// <summary>
        /// Identificador da Loja
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [DataMember]
        public ObjectId ID { get; set; }

        [DataMember]
        public Guid StoreID { get; set; }

        [DataMember(Name = "StoreID", EmitDefaultValue = false)]
        public string StoreIDText
        {
            get { return StoreID.ToString(); }
        }

        /// <summary>
        /// Nome da Loja
        /// </summary>
        [DataMember]
        public string StoreName { get; set; }

        /// <summary>
        /// CNPJ da Loja
        /// </summary>
        [DataMember]
        public string CNPJ { get; set; }

        /// <summary>
        /// Loja opta por Antifraude.
        /// </summary>
        [DataMember]
        public bool AntiFraude { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary>
        [DataMember]
        public DateTime DateCreation { get; set; }

        /// <summary>
        /// Lista de adquirentes da Loja
        /// </summary>
        [DataMember]
        public List<AdquirenteModel> ListaAdquirente { get; set; }


    }
}
