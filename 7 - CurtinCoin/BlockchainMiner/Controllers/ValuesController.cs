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
    public class ValuesController : ApiController
    {
        [Route("api/GenerateBlock")]
        [HttpPost]
        public void Post([FromBody] Transaction transaction)
        {
            //Validate the transaction details

            //Insert transaction details into a block
            Block block = new Block();
            block.Amount = transaction.Amount;
            block.SenderID = transaction.SenderID;
            block.ReceiverID = transaction.ReceiverID;

            //Pull down last block from current blockchain and insert hash into new block
            const string BlockchainServerURL = "http://localhost:63894/";
            RestClient client = new RestClient(BlockchainServerURL);
            RestRequest request = new RestRequest("api/Blockchain/Last");
            IRestResponse response = client.Get(request);
            Block lastBlock = JsonConvert.DeserializeObject<Block>(response.Content);

            block.BlockID = lastBlock.BlockID + 1;
            block.PreviousHash = lastBlock.Hash;

            //Create new hash
            string hashInput = $"{block.BlockID}{block.SenderID}{block.ReceiverID}{block.Amount}{block.PreviousHash}";
            HashGenerator hashGenenerator = new HashGenerator();
            hashGenenerator.GenerateHash(hashInput, out block.Offset, out block.Hash);

            //Submit block to server for inclusion into blockchain
            RestRequest request2 = new RestRequest("api/Blockchain/New");
            request2.AddJsonBody(block);
            IRestResponse response2 = client.Post(request2);
        }
    }
}
