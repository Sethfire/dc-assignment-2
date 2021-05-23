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
        //Retrieve All Transactions
        [Route("api/Transaction")]
        [HttpGet]
        public HttpResponseMessage GetTransactions()
        {
            List<uint> transactions = Models.Transaction.GetTransactions();
            return Request.CreateResponse(HttpStatusCode.OK, transactions);
        }

        //Retrieve Transaction
        [Route("api/Transaction/{transactionID}")]
        [HttpGet]
        public HttpResponseMessage GetTransaction(uint transactionID)
        {
            TransactionStruct transaction = Models.Transaction.GetTransaction(transactionID);
            if (transaction == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Transaction with ID {transactionID} not found");

            return Request.CreateResponse(HttpStatusCode.OK, transaction);
        }

        //Create new Transaction
        [Route("api/Transaction/New")]
        [HttpPost]
        public HttpResponseMessage PostTransaction([FromBody]TransactionStruct transaction)
        {
            uint transactionID = Models.Transaction.CreateTransaction(transaction);
            return Request.CreateResponse(HttpStatusCode.OK, transactionID);
        }
    }
}