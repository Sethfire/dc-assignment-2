using BlockchainLibrary;
using BlockchainServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlockchainServer.Controllers
{
    public class ValuesController : ApiController
    {
        [Route("api/Client")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<ClientStruct> clients = Client.GetClients();
            return Request.CreateResponse(HttpStatusCode.OK, clients);
        }

        [Route("api/Client/New")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] ClientStruct client)
        {
            Client.AddClient(client);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
