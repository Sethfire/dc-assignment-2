using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankDB;

namespace Data_Tier.Model
{
    public class Bank
    {
        private static Bank instance = null;
        public static Bank GetInstance()
        {
            if (instance == null) instance = new Bank();
            return instance;
        }

        private BankDB.BankDB bankInterface = null;
        private AccountAccessInterface accountInterface;
        private UserAccessInterface userAccess;
        private TransactionAccessInterface transactionInterface;
        private Bank()
        {
            bankInterface = new BankDB.BankDB();
            accountInterface = bankInterface.GetAccountInterface();
            userAccess = bankInterface.GetUserAccess();
            transactionInterface = bankInterface.GetTransactionInterface();
        }

        public BankDB.AccountAccessInterface GetAccountInterface()
        {
            return accountInterface;
        }
        public BankDB.UserAccessInterface GetUserAccess()
        {
            return userAccess;
        }
        public BankDB.TransactionAccessInterface GetTransactionInterface()
        {
            return transactionInterface;
        }

        public void ProcessAllTransactions()
        {
            bankInterface.ProcessAllTransactions();
        }
        public void SaveToDisk()
        {
            bankInterface.SaveToDisk();
        }
    }
}