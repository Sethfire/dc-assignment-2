using BlockchainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainClient
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class ClientServer : ClientServerInterface
    {
        public List<Block> GetCurrentBlockchain()
        {
            return Blockchain.GetCurrentBlockchain();
        }

        public Block GetCurrentBlock()
        {
            return Blockchain.GetCurrentBlock();
        }

        public void ReceiveNewTransaction(Transaction transaction)
        {
            Console.WriteLine($"[{Blockchain.MyClientName}Server] Received new transaction");
            Transactions.AddNewTransaction(transaction);
        }
    }
}
