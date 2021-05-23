using APILibrary;
using BankDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseServer.Models
{
    public static class Account
    {
        private static AccountAccessInterface accountInterface;

        static Account()
        {
            accountInterface = Bank.GetAccountInterface();
        }

        public static List<uint> GetAccountIDsByUser(uint userID)
        {
            return accountInterface.GetAccountIDsByUser(userID);
        }

        public static AccountStruct GetAccount(uint accountID)
        {
            AccountStruct account = null;
            accountInterface.SelectAccount(accountID);
            try
            {
                var owner = accountInterface.GetOwner();
                var balance = accountInterface.GetBalance();
                account = new AccountStruct(accountID, owner, balance);
            }
            catch (Exception e)
            {
                //Just ignore
            }

            return account;
        }

        public static uint CreateAccount(AccountStruct account)
        {
            uint accountID = accountInterface.CreateAccount(account.Owner);
            accountInterface.SelectAccount(accountID);
            accountInterface.Deposit(account.Balance);

            return accountID;
        }

        public static void Deposit(uint accountID, uint amount)
        {
            accountInterface.SelectAccount(accountID);
            accountInterface.Deposit(amount);
        }

        public static void Withdraw(uint accountID, uint amount)
        {
            accountInterface.SelectAccount(accountID);
            accountInterface.Withdraw(amount);
        }
    }
}