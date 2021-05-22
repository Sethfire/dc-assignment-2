using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
{
    public class Block
    {
        //Uniquely identifies the block
        public uint BlockID;
        //Identifies the account the transaction is from
        public uint SenderID;
        //Identifies the account the transaction is to
        public uint ReceiverID;
        //Represents the amount of coins being sent (Cannot be negative)
        public float Amount;
        //Used to produce a valid hash (Must be a multiple of 5)
        public uint Offset;
        //The hash of the block immediately prior to this one
        public string PreviousHash;
        //Hash of the current block (Must start with 12345)
        public string Hash;
    }
}
