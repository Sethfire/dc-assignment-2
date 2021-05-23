using APILibrary;
using BankDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseServer.Models
{
    public static class Transaction
    {
        private static TransactionAccessInterface transactionInterface;

        static Transaction()
        {
            transactionInterface = Bank.GetTransactionInterface();
        }

        public static List<uint> GetTransactions()
        {
            return transactionInterface.GetTransactions();
        }

        public static TransactionStruct GetTransaction(uint transactionID)
        {
            TransactionStruct transaction = null;
            transactionInterface.SelectTransaction(transactionID);
            try
            {
                var amount = transactionInterface.GetAmount();
                var senderID = transactionInterface.GetSendrAcct();
                var receiverID = transactionInterface.GetRecvrAcct();
                transaction = new TransactionStruct(transactionID, amount, senderID, receiverID);
            }
            catch (Exception e)
            {
                //Just ignore
            }

            return transaction;
        }

        public static uint CreateTransaction(TransactionStruct transaction)
        {
            uint transactionID = transactionInterface.CreateTransaction();
            transactionInterface.SelectTransaction(transactionID);

            transactionInterface.SetAmount(transaction.Amount);
            transactionInterface.SetSendr(transaction.SenderID);
            transactionInterface.SetRecvr(transaction.ReceiverID);

            return transactionID;
        }
    }
}