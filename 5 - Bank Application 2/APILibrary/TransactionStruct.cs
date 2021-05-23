using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APILibrary
{
    public class TransactionStruct
    {
        public uint TransactionID;
        public uint Amount;
        public uint SenderID;
        public uint ReceiverID;

        public TransactionStruct(uint transactionID, uint amount, uint senderID, uint receiverID)
        {
            TransactionID = transactionID;
            Amount = amount;
            SenderID = senderID;
            ReceiverID = receiverID;
        }
    }
}
