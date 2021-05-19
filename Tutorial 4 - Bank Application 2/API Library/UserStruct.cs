using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Library
{
    public class UserStruct
    {
        public uint userID;
        public string Fname;
        public string Lname;

        public UserStruct(uint userID, string Fname, string Lname)
        {
            this.userID = userID;
            this.Fname = Fname;
            this.Lname = Lname;
        }
    }
}
