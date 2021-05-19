using API_Library;
using BankDB;
using Data_Tier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Data_Tier.Controllers
{
    public class TransactionController : ApiController
    {
        // Get Transactions
        [Route("api/Transactions")]
        [HttpGet]
        public List<uint> GetTransaction()
        {
            Bank bank = Bank.GetInstance();
            TransactionAccessInterface transactionInterface = bank.GetTransactionInterface();

            return transactionInterface.GetTransactions();
        }

        // Get Transaction
        [Route("api/Transaction/{transactionID}")]
        [HttpGet]
        public TransactionStruct GetTransaction(uint transactionID)
        {
            Bank bank = Bank.GetInstance();
            TransactionAccessInterface transactionInterface = bank.GetTransactionInterface();

            transactionInterface.SelectTransaction(transactionID);
            return new TransactionStruct(transactionID, transactionInterface.GetAmount(), transactionInterface.GetSendrAcct(), transactionInterface.GetRecvrAcct());
        }

        // Create new Transaction
        [Route("api/Transaction/new")]
        [HttpPost]
        public void PostNewTransaction(TransactionStruct transaction)
        {
            Bank bank = Bank.GetInstance();
            TransactionAccessInterface transactionInterface = bank.GetTransactionInterface();

            uint transactionID = transactionInterface.CreateTransaction();
            transactionInterface.SelectTransaction(transactionID);

            transactionInterface.SetAmount(transaction.Amount);
            transactionInterface.SetSendr(transaction.SenderID);
            transactionInterface.SetRecvr(transaction.ReceiverID);
        }
    }
}