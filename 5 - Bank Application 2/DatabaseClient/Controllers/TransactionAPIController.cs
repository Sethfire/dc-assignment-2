using APILibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseClient.Controllers
{
    public class TransactionAPIController : ApiController
    {
        [Route("api/Transaction/{transactionID}")]
        [HttpGet]
        public HttpResponseMessage GetTransaction(uint transactionID)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            var request = new RestRequest("api/Transaction/" + transactionID);
            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Request.CreateResponse(HttpStatusCode.NotFound, JsonConvert.DeserializeObject<string>(response.Content));

            TransactionStruct transaction = JsonConvert.DeserializeObject<TransactionStruct>(response.Content);
            return Request.CreateResponse(HttpStatusCode.OK, transaction);
        }

        [Route("api/Transaction/New")]
        [HttpPost]
        public HttpResponseMessage CreateTransaction([FromBody] TransactionStruct transaction)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            var request = new RestRequest("api/Transaction/New");
            request.AddJsonBody(transaction);
            var response = client.Post(request);
            uint transactionID = JsonConvert.DeserializeObject<uint>(response.Content);

            RestRequest request2 = new RestRequest("api/ProcessAllTransactions");
            IRestResponse response2 = client.Get(request2);
            return Request.CreateResponse(HttpStatusCode.OK, transactionID);
        }
    }
}