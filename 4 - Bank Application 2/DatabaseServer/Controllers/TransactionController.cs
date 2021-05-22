using APILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseServer.Controllers
{
    public class TransactionController : ApiController
    {
        //Get All Transactions
        [Route("api/Transaction")]
        [HttpGet]
        public List<uint> GetTransactions()
        {
            return Models.Transaction.GetTransactions();
        }

        //Get Transaction
        [Route("api/Transaction/{userID}")]
        [HttpGet]
        public TransactionStruct GetTransaction(uint transactionID)
        {
            return Models.Transaction.GetTransaction(transactionID);
        }

        //Create new Transaction
        [Route("api/Transaction/new")]
        [HttpPost]
        public void PostTransaction([FromBody]TransactionStruct transaction)
        {
            Models.Transaction.CreateTransaction(transaction);
        }
    }
}