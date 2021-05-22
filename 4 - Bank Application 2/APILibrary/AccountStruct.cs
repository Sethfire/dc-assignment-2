using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary
{
    public class AccountStruct
    {
        public uint AccountID;
        public uint Owner;
        public uint Balance;

        public AccountStruct(uint accountID, uint owner, uint balance)
        {
            AccountID = accountID;
            Owner = owner;
            Balance = balance;
        }
    }
}
