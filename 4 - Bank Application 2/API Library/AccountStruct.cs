using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Library
{
    public class AccountStruct
    {
        public uint AccountID;
        public uint Owner;
        public uint Balance;

        public AccountStruct(uint accountID, uint owner, uint balance)
        {
            this.AccountID = accountID;
            this.Owner = owner;
            this.Balance = balance;
        }
    }
}
