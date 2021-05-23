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
        //Retrieve Accounts by User
        [Route("api/User/{userID}/Account")]
        [HttpGet]
        public HttpResponseMessage GetAccountIDsByUser(uint userID)
        {
            List<uint> accounts = Models.Account.GetAccountIDsByUser(userID);
            return Request.CreateResponse(HttpStatusCode.OK, accounts);
        }

        //Retrieve Account
        [Route("api/Account/{accountID}")]
        [HttpGet]
        public HttpResponseMessage GetAccount(uint accountID)
        {
            AccountStruct account = Models.Account.GetAccount(accountID);
            if (account == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Account with ID {accountID} not found");

            return Request.CreateResponse(HttpStatusCode.OK, account);
        }

        //Create new Account
        [Route("api/Account/New")]
        [HttpPost]
        public HttpResponseMessage PostAccount([FromBody]AccountStruct account)
        {
            Models.Account.CreateAccount(account);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //Deposit
        [Route("api/Account/{accountID}/Deposit/{amount}")]
        [HttpGet]
        public HttpResponseMessage Deposit(uint accountID, uint amount)
        {
            Models.Account.Deposit(accountID, amount);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //Withdraw
        [Route("api/Account/{accountID}/Withdraw/{amount}")]
        [HttpGet]
        public HttpResponseMessage Withdraw(uint accountID, uint amount)
        {
            Models.Account.Withdraw(accountID, amount);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}