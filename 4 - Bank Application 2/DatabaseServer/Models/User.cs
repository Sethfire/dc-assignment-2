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
            userAccess.SelectUser(userID);
            userAccess.GetUserName(out string fName, out string lName);

            return new UserStruct(userID, fName, lName);
        }

        public static void CreateUser(UserStruct user)
        {
            uint id = userAccess.CreateUser();
            userAccess.SelectUser(id);
            userAccess.SetUserName(user.FName, user.LName);
        }
    }
}