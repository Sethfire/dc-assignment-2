using BankDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseServer.Models
{
    public static class Bank
    {
        private static BankDB.BankDB bank;

        static Bank()
        {
            bank = new BankDB.BankDB();
        }

        public static AccountAccessInterface GetAccountInterface()
        {
            return bank.GetAccountInterface();
        }

        public static UserAccessInterface GetUserAccess()
        {
            return bank.GetUserAccess();
        }

        public static TransactionAccessInterface GetTransactionInterface()
        {
            return bank.GetTransactionInterface();
        }

        public static void ProcessAllTransactions()
        {
            bank.ProcessAllTransactions();
        }

        public static void SaveToDisk()
        {
            bank.SaveToDisk();
        }
    }
}