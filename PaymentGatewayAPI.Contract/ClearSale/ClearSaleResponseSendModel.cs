using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract.ClearSale
{
    /// <summary>
    /// Classe para o Retorno da RequestSend do ClearSale.
    /// </summary>
    [DataContract]
    public class ClearSaleResponseSendModel
    {
        /// <summary>
        /// Status da Order
        /// </summary>
        [DataMember]
        public List<OrderStatus> Orders { get; set; }

        /// <summary>
        /// Id da transação
        /// </summary>
        [DataMember]
        public string TransactionID { get; set; }

    }

    /// <summary>
    /// Classe com os Status possíveis para o retorno da Order
    
    /// </summary>
    [DataContract]
    public class OrderStatus
    {
        /// <summary>
        /// Order ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// Status of the order:
        /// <para/>APA Automatic Approval – the order was automatically approved according to the rules defined.
        /// <para/>APM Manual Approval – the order was manually approved by a risk analyst.
        /// <para/>RPM Denied without prejudice – Order was denied with no indication of fraud. Usually due to impossibility of contact or invalid document.
        /// <para/>AMA Manual Analysis – the order was sent to a manual analysis queue.
        /// <para/>ERR Error – An error occurred during the integration. It is necessary to analyze the XML and resend after fixing it.
        /// <para/>NVO New Order – the order was imported successfully and was not yet classified.
        /// <para/>SUS Fraud Suspicion – the order was denied due to a suspicion of fraud, usually based on contact with the customer or behavior registered in the ClearSale database.
        /// <para/>CAN Customer asked for cancellation– the customer asked an analyst to cancel the purchase.
        /// <para/>FRD Confirmed Fraud – The order was analyzed and, following contact made, the credit card company confirmed fraud or the owner of the credit card denied awareness of the purchase.
        /// <para/>RPA Automatically Denied – the order was denied according to a pre-defined rule (not recommended).
        /// <para/>RPP Denied by Policy – The order was denied due to a policy defined by ClearSale or the client.
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// Order fraud score
        /// </summary>
        [DataMember]
        public string Score { get; set; }
    }



}
