using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Library
{
    public class TransactionStruct
    {
        public uint TransactionID;
        public uint Amount;
        public uint SenderID;
        public uint ReceiverID;

        public TransactionStruct(uint transactionID, uint amount, uint senderID, uint receiverID)
        {
            this.TransactionID = transactionID;
            this.Amount = amount;
            this.SenderID = senderID;
            this.ReceiverID = receiverID;
        }
    }
}
