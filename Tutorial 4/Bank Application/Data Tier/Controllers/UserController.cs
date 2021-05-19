using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Library;
using BankDB;
using Data_Tier.Model;

namespace Data_Tier.Controllers
{
    public class UserController : ApiController
    {
        //Get All Users
        [Route("api/Users")]
        [HttpGet]
        public List<uint> GetUsers()
        {
            Bank bank = Bank.GetInstance();
            UserAccessInterface userAccess = bank.GetUserAccess();

            return userAccess.GetUsers();
        }

        // Get User
        [Route("api/User/{userID}")]
        [HttpGet]
        public UserStruct GetUser(uint userID)
        {
            Bank bank = Bank.GetInstance();
            UserAccessInterface userAccess = bank.GetUserAccess();

            userAccess.SelectUser(userID);

            string fname, lname;

            userAccess.GetUserName(out fname, out lname);

            return new UserStruct(userID, fname, lname);
        }

        // Get Accounts by User
        [Route("api/User/{userID}/Accounts")]
        [HttpGet]
        public List<uint> GetAccountsByUser(uint userID)
        {
            Bank bank = Bank.GetInstance();
            AccountAccessInterface accountInterface = bank.GetAccountInterface();

            return accountInterface.GetAccountIDsByUser(userID);
        }

        // Create new User
        [Route("api/User/new")]
        [HttpPost]
        public void PostNewUser(UserStruct user)
        {
            Bank bank = Bank.GetInstance();
            UserAccessInterface userAccess = bank.GetUserAccess();

            uint id = userAccess.CreateUser();
            userAccess.SelectUser(id);
            userAccess.SetUserName(user.Fname, user.Lname);
        }
    }
}