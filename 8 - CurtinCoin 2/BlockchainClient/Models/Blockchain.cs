using BlockchainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainClient
{
    public static class Blockchain
    {
        public static string MyClientName;
        private static List<Block> blockchain;

        static Blockchain()
        {
            blockchain = new List<Block>();

            Block block = new Block();
            block.BlockID = 0;
            block.ReceiverID = 0;
            block.SenderID = 0;
            block.Amount = 0;
            block.PreviousHash = "";

            HashGenerator hashGenerator = new HashGenerator();
            string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}";
            hashGenerator.GenerateHash(hashInput, out block.Offset, out block.Hash);

            AddBlock(block);
        }

        public static List<Block> GetCurrentBlockchain()
        {
            return blockchain;
        }

        public static int GetNumberOfBlocks()
        {
            return blockchain.Count();
        }

        public static void ReplaceCurrentBlockchain(List<Block> newBlockchain)
        {
            blockchain = newBlockchain;
        }

        public static Block GetCurrentBlock()
        {
            return blockchain.Last();
        }

        public static void AddBlock(Block block)
        {
            blockchain.Add(block);
        }

        public static float GetUserBalance(uint userID)
        {
            float balance = 0;
            foreach (Block block in blockchain)
            {
                if (block.SenderID == userID)
                    balance = balance - block.Amount;
                else if (block.ReceiverID == userID)
                    balance = balance + block.Amount;
            }
            return balance;
        }
    }
}
