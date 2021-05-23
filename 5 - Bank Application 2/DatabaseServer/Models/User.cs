using APILibrary;
using BankDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatabaseServer.Models
{
    public static class User
    {
        private static UserAccessInterface userAccess;

        static User()
        {
            userAccess = Bank.GetUserAccess();
        }

        public static List<uint> GetUsers()
        {
            return userAccess.GetUsers();
        }

        public static UserStruct GetUser(uint userID)
        {
            UserStruct user = null;
            userAccess.SelectUser(userID);
            try
            {
                userAccess.GetUserName(out string fName, out string lName);
                user = new UserStruct(userID, fName, lName);
            }
            catch(Exception e)
            {
                //Just ignore
            }

            return user;
        }

        public static uint CreateUser(UserStruct user)
        {
            uint userID = userAccess.CreateUser();
            userAccess.SelectUser(userID);
            userAccess.SetUserName(user.FName, user.LName);

            return userID;
        }
    }
}