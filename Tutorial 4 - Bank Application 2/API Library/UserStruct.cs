using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Library
{
    public class UserStruct
    {
        public uint UserID;
        public string FName;
        public string LName;

        public UserStruct(uint UserID, string FName, string LName)
        {
            this.UserID = UserID;
            this.FName = FName;
            this.LName = LName;
        }
    }
}
