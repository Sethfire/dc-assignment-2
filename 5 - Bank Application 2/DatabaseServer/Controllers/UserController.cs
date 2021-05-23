using APILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseServer.Controllers
{
    public class UserController : ApiController
    {
        //Retrieve All Users
        [Route("api/User")]
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {
            List<uint> users = Models.User.GetUsers();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        //Retrieve User
        [Route("api/User/{userID}")]
        [HttpGet]
        public HttpResponseMessage GetUser(uint userID)
        {
            UserStruct user = Models.User.GetUser(userID);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"User with ID {userID} not found");

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        //Create new User
        [Route("api/User/New")]
        [HttpPost]
        public HttpResponseMessage PostUser([FromBody]UserStruct user)
        {
            uint userID = Models.User.CreateUser(user);
            return Request.CreateResponse(HttpStatusCode.OK, userID);
        }
    }
}