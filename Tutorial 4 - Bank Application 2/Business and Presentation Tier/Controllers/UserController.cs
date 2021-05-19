using API_Library;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Business_and_Presentation_Tier.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/User/{userID}")]
        [HttpGet]
        public UserStruct GetUser(uint userID)
        {
            RestClient client = new RestClient("http://localhost:60333/");

            RestRequest request = new RestRequest("api/user/"+userID);
            IRestResponse response = client.Get(request);

            UserStruct user = JsonConvert.DeserializeObject<UserStruct>(response.Content);
            return user;
        }

        [Route("api/User/new")]
        [HttpPost]
        public void PostNewUser()
        {
            RestClient client = new RestClient("http://localhost:60333/");

            RestRequest request = new RestRequest("api/user/new");
            IRestResponse response = client.Post(request);

            RestRequest request2 = new RestRequest("api/SaveToDisk");
            IRestResponse response2 = client.Get(request);
        }
    }
}