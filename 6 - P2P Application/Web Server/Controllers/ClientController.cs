using API_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Server.Models;

namespace Web_Server.Controllers
{
    public class ClientController : ApiController
    {
        [Route("api/Client")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<ClientStruct> clients = Clients.GetClients();
            return Request.CreateResponse(HttpStatusCode.OK, clients);
        }

        [Route("api/Client/New")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] ClientStruct client)
        {
            Clients.AddClient(client);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}