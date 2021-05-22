using BlockchainLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainServer.Models
{
    public static class Blockchain
    {
        private static List<Block> blockchain;
        private static Block lastBlock;

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
            lastBlock = block;
        }

        public static List<Block> GetBlockchain()
        {
            return blockchain;
        }

        public static BlockchainState GetBlockchainState()
        {
            BlockchainState blockchainState = new BlockchainState();
            blockchainState.NumberOfBlocks = blockchain.Count();
            return blockchainState;
        }

        public static Block GetLastBlock()
        {
            return lastBlock;
        }

        public static void AddBlock(Block block)
        {
            System.Diagnostics.Debug.WriteLine($"Adding new block {block.BlockID} to blockchain");
            blockchain.Add(block);
            lastBlock = block;
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