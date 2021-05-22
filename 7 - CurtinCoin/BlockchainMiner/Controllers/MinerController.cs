using BlockchainLibrary;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace BlockchainMiner.Controllers
{
    public class MinerController : ApiController
    {
        [Route("api/Mine")]
        [HttpPost]
        public IHttpActionResult Mine([FromBody] Transaction transaction)
        {
            const string BlockchainServerURL = "http://localhost:63894/";
            RestClient client = new RestClient(BlockchainServerURL);

            RestRequest request = new RestRequest($"api/Blockchain/User/{transaction.SenderID}");
            IRestResponse response = client.Get(request);
            float senderBalance = JsonConvert.DeserializeObject<float>(response.Content);

            //Validate that the sender has enough money in their account
            if (senderBalance < transaction.Amount) return BadRequest();

            //Insert transaction details into a block
            Block block = new Block();
            block.Amount = transaction.Amount;
            block.SenderID = transaction.SenderID;
            block.ReceiverID = transaction.ReceiverID;

            //Pull down last block from current blockchain and insert hash into new block
            RestRequest request2 = new RestRequest("api/Blockchain/Last");
            IRestResponse response2 = client.Get(request2);
            Block lastBlock = JsonConvert.DeserializeObject<Block>(response2.Content);

            block.BlockID = lastBlock.BlockID + 1;
            block.PreviousHash = lastBlock.Hash;

            //Create new hash
            string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}";
            HashGenerator hashGenenerator = new HashGenerator();
            hashGenenerator.GenerateHash(hashInput, out block.Offset, out block.Hash);

            //Submit block to server for inclusion into blockchain
            RestRequest request3 = new RestRequest("api/Blockchain/New");
            request2.AddJsonBody(block);
            IRestResponse response3 = client.Post(request3);

            return Ok();
        }
    }
}
