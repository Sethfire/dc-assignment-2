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
        public List<Client> Get()
        {
            return Clients.GetClients();
        }

        [Route("api/Client")]
        [HttpPost]
        public void Post(Client client)
        {
            Clients.AddClient(client);
        }
    }
}