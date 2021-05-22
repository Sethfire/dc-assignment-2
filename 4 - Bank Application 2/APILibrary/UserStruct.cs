using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary
{
    public class UserStruct
    {
        public uint UserID;
        public string FName;
        public string LName;

        public UserStruct(uint userID, string fName, string lName)
        {
            UserID = userID;
            FName = fName;
            LName = lName;
        }
    }
}
