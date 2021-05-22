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
        public class UserAPIController : ApiController
        {
            [Route("api/User/{userID}")]
            [HttpGet]
            public AccountStruct GetAccount(uint accountID)
            {
                RestClient client = new RestClient("http://localhost:57464/");

                RestRequest request = new RestRequest("api/Account/" + accountID);
                IRestResponse response = client.Get(request);

                AccountStruct account = JsonConvert.DeserializeObject<AccountStruct>(response.Content);
                return account;
            }

            [Route("api/User/new")]
            [HttpPost]
            public void CreateAccount([FromBody] AccountStruct account)
            {
                

                RestClient client = new RestClient("http://localhost:57464/");

                RestRequest request = new RestRequest("api/Account/new");
                request.AddJsonBody(account);
                IRestResponse response = client.Post(request);

                RestRequest request2 = new RestRequest("api/SaveToDisk");
                IRestResponse response2 = client.Get(request2);
            }
        }
    }
}