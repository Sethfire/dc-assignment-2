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
    public class UserAPIController : ApiController
    {
        [Route("api/User/{userID}")]
        [HttpGet]
        public UserStruct GetUser(uint userID)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            RestRequest request = new RestRequest("api/User/" + userID);
            IRestResponse response = client.Get(request);

            UserStruct user = JsonConvert.DeserializeObject<UserStruct>(response.Content);
            return user;
        }

        [Route("api/User/new")]
        [HttpPost]
        public void CreateUser([FromBody] UserStruct user)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            RestRequest request = new RestRequest("api/User/new");
            request.AddJsonBody(user);
            IRestResponse response = client.Post(request);

            RestRequest request2 = new RestRequest("api/SaveToDisk");
            IRestResponse response2 = client.Get(request2);
        }
    }
}