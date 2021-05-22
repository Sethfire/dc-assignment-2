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
        //Get All Users
        [Route("api/User")]
        [HttpGet]
        public List<uint> GetUsers()
        {
            return Models.User.GetUsers();
        }

        //Get User
        [Route("api/User/{userID}")]
        [HttpGet]
        public UserStruct GetUser(uint userID)
        {
            return Models.User.GetUser(userID);
        }

        //Create new User
        [Route("api/User/new")]
        [HttpPost]
        public void PostUser([FromBody]UserStruct user)
        {
            Models.User.CreateUser(user);
        }
    }
}