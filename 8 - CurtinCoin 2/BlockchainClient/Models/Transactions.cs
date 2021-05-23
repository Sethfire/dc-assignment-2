using BlockchainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainClient
{
    public static class Transactions
    {
        private static List<Transaction> transactions;

        static Transactions()
        {
            transactions = new List<Transaction>();
        }

        public static int GetTransactionCount()
        {
            return transactions.Count();
        }

        public static Transaction GetCurrentTransaction()
        {

            Transaction transaction = transactions.Last();
            transactions.Remove(transaction);
            return transaction;
        }

        public static void AddNewTransaction(Transaction transaction)
        {
            float senderBalance = Blockchain.GetUserBalance(transaction.SenderID);
            if ((transaction.SenderID != 0) && (senderBalance < transaction.Amount))
                throw new Exception("Sender does not have enough coins");

            transactions.Add(transaction);
        }
    }
}
