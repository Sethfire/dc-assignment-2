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
    public class AccountController : ApiController
    {
        // Get Account Details
        [Route("api/Account/{accountID}")]
        [HttpGet]
        public AccountStruct GetAccountDetails(uint accountID)
        {
            Bank bank = Bank.GetInstance();
            AccountAccessInterface accountInterface = bank.GetAccountInterface();

            accountInterface.SelectAccount(accountID);
            return new AccountStruct(accountID, accountInterface.GetOwner(), accountInterface.GetBalance());
        }

        // Create new Account
        [Route("api/Account/new")]
        [HttpPost]
        public void PostNewAccount(AccountStruct account)
        {
            Bank bank = Bank.GetInstance();
            AccountAccessInterface accountInterface = bank.GetAccountInterface();

            uint accountID = accountInterface.CreateAccount(account.Owner);

            accountInterface.SelectAccount(accountID);
            accountInterface.Deposit(account.Balance);
        }

        // Deposit Amount
        [Route("api/Account/{accountID}/Deposit/{amount}")]
        [HttpGet]
        public void Deposit(uint accountID, uint amount)
        {
            Bank bank = Bank.GetInstance();
            AccountAccessInterface accountInterface = bank.GetAccountInterface();

            accountInterface.SelectAccount(accountID);
            accountInterface.Deposit(amount);
        }

        // Withdraw Amount
        [Route("api/Account/{accountID}/Withdraw/{amount}")]
        [HttpGet]
        public void Withdraw(uint accountID, uint amount)
        {
            Bank bank = Bank.GetInstance();
            AccountAccessInterface accountInterface = bank.GetAccountInterface();

            accountInterface.SelectAccount(accountID);
            accountInterface.Withdraw(amount);
        }
    }
}