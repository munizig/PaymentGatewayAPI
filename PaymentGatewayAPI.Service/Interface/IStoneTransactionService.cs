﻿using System.Threading.Tasks;
using GatewayApiClient.DataContracts;
using GatewayApiClient.Utility;
using PaymentGatewayAPI.Contract;

namespace PaymentGatewayAPI.Service.Interface
{
    public interface IStoneTransactionService
    {
        HttpResponse CreateCreditCardTransaction(TransactionModel transactionModel);
        void GetTransaction();
    }
}