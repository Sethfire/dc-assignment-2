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
            transactionInterface.SelectTransaction(transactionID);

            return new TransactionStruct(transactionID, transactionInterface.GetAmount(), transactionInterface.GetSendrAcct(), transactionInterface.GetRecvrAcct());
        }

        public static void CreateTransaction(TransactionStruct transaction)
        {
            uint transactionID = transactionInterface.CreateTransaction();
            transactionInterface.SelectTransaction(transactionID);

            transactionInterface.SetAmount(transaction.Amount);
            transactionInterface.SetSendr(transaction.SenderID);
            transactionInterface.SetRecvr(transaction.ReceiverID);
        }
    }
}