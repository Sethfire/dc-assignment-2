using APILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseServer.Controllers
{
    public class AccountController : ApiController
    {
        //Get Accounts by User
        [Route("api/User/{userID}/Account")]
        [HttpGet]
        public List<uint> GetAccountIDsByUser(uint userID)
        {
            return Models.Account.GetAccountIDsByUser(userID);
        }

        //Get Account
        [Route("api/Account/{accountID}")]
        [HttpGet]
        public AccountStruct GetAccount(uint accountID)
        {
            return Models.Account.GetAccount(accountID);
        }

        //Create new Account
        [Route("api/Account/new")]
        [HttpPost]
        public void PostAccount([FromBody]AccountStruct account)
        {
            Models.Account.CreateAccount(account);
        }

        //Deposit
        [Route("api/Account/{accountID}/Deposit/{amount}")]
        [HttpGet]
        public void Deposit(uint accountID, uint amount)
        {
            Models.Account.Deposit(accountID, amount);
        }

        //Withdraw
        [Route("api/Account/{accountID}/Withdraw/{amount}")]
        [HttpGet]
        public void Withdraw(uint accountID, uint amount)
        {
            Models.Account.Withdraw(accountID, amount);
        }
    }
}