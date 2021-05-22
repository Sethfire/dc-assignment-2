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

        static Blockchain()
        {
            blockchain = new List<Block>();

            //
            Block block = new Block();
            block.BlockID = 0;
            block.ReceiverID = 0;
            block.SenderID = 0;
            block.Amount = 0;

            //Generate hash?
            //block.BlockOffset = 0;
            //block.PreviousHash = 0;
            //block.Hash = 0;
        }

        public static BlockchainState GetBlockchainState()
        {
            BlockchainState blockchainState = new BlockchainState();
            blockchainState.NumberOfBlocks = blockchain.Count();
            return blockchainState;
        }

        public static Block GetLastBlock()
        {
            return blockchain.Last();
        }

        public static void AddBlock(Block block)
        {
            //Make sure to do some checking though
            blockchain.Add(block);
        }

        public static float GetUserBalance(uint userID)
        {
            float balance = 0;

            foreach (Block block in blockchain)
            {
                if (block.SenderID == userID)
                {
                    balance = balance - block.Amount;
                }
                else if (block.ReceiverID == userID)
                {
                    balance = balance + block.Amount;
                }
            }

            return balance;
        }
    }
}