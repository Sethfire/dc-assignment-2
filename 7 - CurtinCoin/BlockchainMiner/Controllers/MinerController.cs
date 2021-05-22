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
        public const string BlockchainServerURL = "http://localhost:63894/";

        [Route("api/Mine")]
        [HttpPost]
        public HttpResponseMessage Mine([FromBody] Transaction transaction)
        {
            RestClient client = new RestClient(BlockchainServerURL);

            //Retrieve the balance of the sender's wallet
            var request = new RestRequest($"api/Blockchain/User/{transaction.SenderID}");
            var response = client.Get(request);

            //Retrieve the last block from current blockchain
            var request2 = new RestRequest("api/Blockchain/Last");
            var response2 = client.Get(request2);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to retrieve sender wallet");

            if (response2.StatusCode == HttpStatusCode.BadRequest)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable to retrieve last block");

            //Validate that the sender has enough coins in their account (With the sole exception being ID 0, the bank)
            float senderBalance = JsonConvert.DeserializeObject<float>(response.Content);
            if ((transaction.SenderID != 0) && (senderBalance < transaction.Amount))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Sender does not have enough coins");

            //Create and Insert transaction details into a block
            Block block = new Block();
            block.Amount = transaction.Amount;
            block.SenderID = transaction.SenderID;
            block.ReceiverID = transaction.ReceiverID;

            //Insert previous block hash into the block
            Block lastBlock = JsonConvert.DeserializeObject<Block>(response2.Content);
            block.BlockID = lastBlock.BlockID + 1;
            block.PreviousHash = lastBlock.Hash;

            //Create a new hash
            HashGenerator hashGenenerator = new HashGenerator();
            string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}";
            hashGenenerator.GenerateHash(hashInput, out block.Offset, out block.Hash);

            //Submit block to server for inclusion into blockchain
            var request3 = new RestRequest("api/Blockchain/New");
            request3.AddJsonBody(block);
            var response3 = client.Post(request3);

            if (response3.StatusCode == HttpStatusCode.BadRequest)
                return Request.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.DeserializeObject<string>(response3.Content));

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
