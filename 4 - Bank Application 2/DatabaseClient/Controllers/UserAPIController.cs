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
        public HttpResponseMessage GetUser(uint userID)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            RestRequest request = new RestRequest("api/User/" + userID);
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return Request.CreateResponse(HttpStatusCode.NotFound, JsonConvert.DeserializeObject<string>(response.Content));

            UserStruct user = JsonConvert.DeserializeObject<UserStruct>(response.Content);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [Route("api/User/New")]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody] UserStruct user)
        {
            RestClient client = new RestClient("http://localhost:57464/");

            RestRequest request = new RestRequest("api/User/New");
            request.AddJsonBody(user);
            IRestResponse response = client.Post(request);
            uint userID = JsonConvert.DeserializeObject<uint>(response.Content);

            RestRequest request2 = new RestRequest("api/SaveToDisk");
            IRestResponse response2 = client.Get(request2);
            return Request.CreateResponse(HttpStatusCode.OK, userID);
        }
    }
}