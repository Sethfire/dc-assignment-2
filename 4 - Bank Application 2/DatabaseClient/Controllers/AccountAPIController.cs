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
    public class AccountAPIController : ApiController
    {
        [Route("api/Account/{accountID}")]
        [HttpGet]
        public HttpResponseMessage GetAccount(uint accountID)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            var request = new RestRequest($"api/Account/{accountID}");
            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Request.CreateResponse(HttpStatusCode.NotFound, JsonConvert.DeserializeObject<string>(response.Content));

            AccountStruct account = JsonConvert.DeserializeObject<AccountStruct>(response.Content);
            return Request.CreateResponse(HttpStatusCode.OK, account);
        }

        [Route("api/Account/new")]
        [HttpPost]
        public HttpResponseMessage CreateAccount([FromBody] AccountStruct account)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            var request = new RestRequest("api/Account/new");
            request.AddJsonBody(account);
            var response = client.Post(request);

            var request2 = new RestRequest("api/SaveToDisk");
            var response2 = client.Get(request2);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Account/{accountID}/Deposit/{amount}")]
        [HttpGet]
        public HttpResponseMessage Deposit(uint accountID, uint amount)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            var request = new RestRequest($"api/Account/{accountID}/Deposit/{amount}");
            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Request.CreateResponse(HttpStatusCode.NotFound, JsonConvert.DeserializeObject<string>(response.Content));

            var request2 = new RestRequest("api/SaveToDisk");
            var response2 = client.Get(request2);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/Account/{accountID}/Withdraw/{amount}")]
        [HttpGet]
        public HttpResponseMessage Withdraw(uint accountID, uint amount)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            var request = new RestRequest($"api/Account/{accountID}/Withdraw/{amount}");
            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Request.CreateResponse(HttpStatusCode.NotFound, JsonConvert.DeserializeObject<string>(response.Content));

            var request2 = new RestRequest("api/SaveToDisk");
            var response2 = client.Get(request2);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}