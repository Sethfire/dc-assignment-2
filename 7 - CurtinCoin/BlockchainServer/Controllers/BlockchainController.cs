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
        //Retrieve the entire blockchain
        [Route("api/Blockchain")]
        [HttpGet]
        public HttpResponseMessage GetBlockchain()
        {
            System.Diagnostics.Debug.WriteLine($"Received GET request to retrieve blockchain");
            return Request.CreateResponse(HttpStatusCode.OK, Blockchain.GetBlockchain());
        }
        //Retrieve the current state of the blockchain
        [Route("api/Blockchain/Status")]
        [HttpGet]
        public HttpResponseMessage GetBlockchainState()
        {
            System.Diagnostics.Debug.WriteLine($"Received GET request to retrieve blockchain status");
            return Request.CreateResponse(HttpStatusCode.OK, Blockchain.GetBlockchainState());
        }

        //Retrieve the last block in the blockchain
        [Route("api/Blockchain/Last")]
        [HttpGet]
        public HttpResponseMessage GetLastBlock()
        {
            System.Diagnostics.Debug.WriteLine($"Received GET request to retrieve last block in blockchain");
            return Request.CreateResponse(HttpStatusCode.OK, Blockchain.GetLastBlock());
        }

        //Retrieve the current coin balance of a given user
        [Route("api/Blockchain/User/{userID}")]
        [HttpGet]
        public HttpResponseMessage GetUserBalance(uint userID)
        {
            System.Diagnostics.Debug.WriteLine($"Received GET request to retrieve coin balance of user {userID}");
            //User ID must be non-negative
            if (userID < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid User ID");

            return Request.CreateResponse(HttpStatusCode.OK, Blockchain.GetUserBalance(userID));
        }

        //Submit a new block to the chain
        [Route("api/Blockchain/New")]
        [HttpPost]
        public HttpResponseMessage AddBlock([FromBody] Block block)
        {
            System.Diagnostics.Debug.WriteLine($"Received POST request to add new block to blockchain");

            //Block ID must be a number higher than other block IDs
            //Since the last block will always have the highest block ID, just compare using that block.
            if (block.BlockID < Blockchain.GetLastBlock().BlockID) 
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Block ID is lower than existing Block IDs");

            //The Sender Wallet ID must have at least as many coins as the transaction amount (Unless it's the bank)
            if ((block.SenderID != 0) && (Blockchain.GetUserBalance(block.SenderID) < block.Amount))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Sender does not have enough coins");

            //The amount must be greater than 0
            if (block.Amount <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Non-positive amount of coins being transacted");

            //The Block Offset must be divisible by 5
            if ((block.Offset % 5) != 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Block Hash Offset not a multiple of 5");

            //The previous block hash must match the last block in the current chain
            if (block.PreviousHash.Equals(Blockchain.GetLastBlock().Hash) == false)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Previous Block hash does not match last block hash");

            //The Block Hash must be valid
            HashGenerator hashGenerator = new HashGenerator();
            if (hashGenerator.ValidateHash(block) == false)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Block Hash invalid");

            //No number can be negative
            if (block.SenderID < 0 || block.ReceiverID < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Wallet IDs are negative");

            Blockchain.AddBlock(block);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
