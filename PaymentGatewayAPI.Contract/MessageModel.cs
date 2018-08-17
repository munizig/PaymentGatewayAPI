using PaymentGatewayAPI.Contract.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PaymentGatewayAPI.Contract
{
    [DataContract]
    public class MessageModel
    {
        private string _messageCode;
        private string _messageText;

        /// <summary>
        /// Classe de retorno das requisições
        /// </summary>
        /// <param name="messageCode">Código da Mensagem.</param>
        /// <param name="transactionID">ID da Transação que foi registrada pela requisição.</param>
        public MessageModel(string messageCode, TipoClasseMensagemEnum tipoMessage, string transactionID = "")
        {
            TransactionID = transactionID;

            Dictionary<string, string> MessageList = new Dictionary<string, string>();

            switch (tipoMessage)
            {
                case TipoClasseMensagemEnum.Transaction:
                    MessageList.Add("ERR", "Não foi possível criar a transação.");
                    MessageList.Add("NEG", "Transação negada pelo sistema AntiFraude.");
                    MessageList.Add("SUC", "Transação efetuada com sucesso.");
                    MessageList.Add("NOT", "Transação não encontrada.");
                    MessageList.Add("NOTADQ", "Não há adquirentes cadastrados para a loja informada.");
                    MessageList.Add("NOTCARD", "Não há cartões vinculados a adquirentes da loja.");
                    MessageList.Add("TIMEOUT", "Tempo limite da transação excedido.");
                    MessageList.Add("OUTCRED", "Transação negada por falta de crédito.");
                    MessageList.Add("UNAUTH", "Transação não autorizada.");
                    break;
                case TipoClasseMensagemEnum.Store:
                    MessageList.Add("NOT", "Loja não encontrada.");
                    MessageList.Add("ERR", "Não foi possível cadastrar a Loja. Verifique os parâmetros e tente novamente.");
                    MessageList.Add("SUC", "Loja cadastrada com sucesso.");
                    break;
            }

            

            _messageCode = messageCode;
            _messageText = MessageList.GetValueOrDefault(_messageCode);
        }

        /// <summary>
        /// Código da mensagem de retorno
        /// </summary>
        [DataMember]
        public string MessageCode { get { return _messageCode; } }

        /// <summary>
        /// Texto da mensagem de retorno
        /// </summary>
        [DataMember]
        public string MessageText { get { return _messageText; } }

        /// <summary>
        /// ID da transação que foi registrada pela requisição
        /// </summary>
        [DataMember]
        public string TransactionID { get; set; }

    }
}
