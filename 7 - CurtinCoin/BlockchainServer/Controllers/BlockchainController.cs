using BlockchainLibrary;
using BlockchainServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BlockchainServer.Controllers
{
    public class BlockchainController : ApiController
    {
        //Get the current state of the blockchain
        [Route("api/Blockchain")]
        [HttpGet]
        public BlockchainState GetBlockchainState()
        {
            return Blockchain.GetBlockchainState();
        }

        [Route("api/Blockchain/Last")]
        [HttpGet]
        public Block GetLastBlock()
        {
            return Blockchain.GetLastBlock();
        }

        //Get the current coin balance of a given user
        [Route("api/Blockchain/User/{userID}")]
        [HttpGet]
        public float GetUserBalance(uint userID)
        {
            return Blockchain.GetUserBalance(userID);
        }

        //Submit a new block to the chain
        [Route("api/Blockchain/New")]
        [HttpGet]
        public IHttpActionResult AddBlock([FromBody] Block block)
        {
            //Block ID must be a number higher than other block IDs
            //Since the last block will always have the highest block ID, just compare using that block.
            if (block.BlockID >= Blockchain.GetLastBlock().BlockID) return BadRequest();

            //The Sender Wallet ID must have at least as many coins as the transaction amount
            if (Blockchain.GetUserBalance(block.SenderID) < block.Amount) return BadRequest();

            //The amount must be greater than 0
            if (block.Amount <= 0) return BadRequest();

            //The Block Offset must be divisible by 5
            if ((block.Offset % 5) != 0) return BadRequest();

            //The previous block hash must match the last block in the current chain
            if (block.PreviousHash.Equals(Blockchain.GetLastBlock().Hash) == false) return BadRequest();

            //The Block Hash must be valid
            HashGenerator hashGenerator = new HashGenerator();
            if (hashGenerator.ValidateHash(block) == false) return BadRequest();

            //No number can be negative
            if (block.SenderID < 0) return BadRequest();
            if (block.ReceiverID < 0) return BadRequest();

            Blockchain.AddBlock(block);
            return Ok();
        }
    }
}
