using DatabaseServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseServer.Controllers
{
    public class AdminController : ApiController
    {
        [Route("api/ProcessAllTransactions")]
        [HttpGet]
        public HttpResponseMessage ProcessAllTransactions()
        {
            Bank.ProcessAllTransactions();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("api/SaveToDisk")]
        [HttpGet]
        public HttpResponseMessage SaveToDisk()
        {
            Bank.SaveToDisk();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}